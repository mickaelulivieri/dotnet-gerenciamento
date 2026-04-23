import { useEffect, useState } from "react";
import { api } from "../services/api";

type Task = {
  id: number;
  title: string;
  description: string;
  status: number;
  priority: number;
  dueDate?: string;
  userId: number;
};

type User = {
  id: number;
  name: string;
};

const priorityMap = ["Low", "Medium", "High"];

export function Dashboard() {
  const [tasks, setTasks] = useState<Task[]>([]);
  const [users, setUsers] = useState<User[]>([]);

  async function load() {
    const [t, u] = await Promise.all([
      api.get("/Task"),
      api.get("/User"),
    ]);

    setTasks(t.data);
    setUsers(u.data);
  }

  async function updateStatus(task: Task, newStatus: number) {
    await api.put(`/Task/${task.id}`, {
      title: task.title,
      description: task.description,
      status: newStatus, // 👈 só isso muda
      priority: task.priority,
      dueDate: task.dueDate || null,
    });

    await load();
  }

  useEffect(() => {
    load();
  }, []);

  return (
    <div style={{ width: "100%" }}>
      <h2 style={{ textAlign: "center", marginBottom: 20 }}>
        Overview
      </h2>

      <div style={{ display: "flex", flexDirection: "column", gap: 20 }}>
        {users.map((user) => {
          const userTasks = tasks.filter((t) => t.userId === user.id);

          return (
            <div
              key={user.id}
              style={{
                background: "#1a1a2e",
                padding: 20,
                borderRadius: 15,
              }}
            >
              <h3 style={{ textAlign: "center" }}>{user.name}</h3>

              {userTasks.map((task) => (
                <div
                  key={task.id}
                  style={{
                    marginTop: 10,
                    padding: 12,
                    borderRadius: 10,
                    background: "#2c2c54",
                    display: "flex",
                    justifyContent: "space-between",
                    alignItems: "center",
                  }}
                >
                  <div>
                    <b>{task.title}</b>
                    <div style={{ fontSize: 12, opacity: 0.7 }}>
                      {priorityMap[task.priority]}
                    </div>
                  </div>

                  <select
                    value={task.status}
                    onChange={(e) =>
                      updateStatus(task, Number(e.target.value))
                    }
                  >
                    <option value={0}>Pending</option>
                    <option value={1}>InProgress</option>
                    <option value={2}>Completed</option>
                  </select>
                </div>
              ))}
            </div>
          );
        })}
      </div>
    </div>
  );
}