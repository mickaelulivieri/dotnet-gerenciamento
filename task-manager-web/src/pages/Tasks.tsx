import { useEffect, useState } from "react";
import { api } from "../services/api";

type Task = {
  id: number;
  title: string;
  description: string;
  priority: number;
  status?: number;
  userId: number;
};

type User = {
  id: number;
  name: string;
};

type TaskForm = {
  title: string;
  description: string;
  priority: "Low" | "Medium" | "High";
  dueDate: string;
  userId: number;
};

const priorityMap = ["Low", "Medium", "High"];

export function Tasks() {
  const [tasks, setTasks] = useState<Task[]>([]);
  const [users, setUsers] = useState<User[]>([]);
  const [showList, setShowList] = useState(false);
  const [loading, setLoading] = useState(false);
  const [editingId, setEditingId] = useState<number | null>(null);
  const [searchId, setSearchId] = useState("");

  const [form, setForm] = useState<TaskForm>({
    title: "",
    description: "",
    priority: "Medium",
    dueDate: "",
    userId: 0,
  });

  useEffect(() => {
    loadUsers();
  }, []);

  async function loadUsers() {
    const res = await api.get<User[]>("/User");
    setUsers(res.data);
  }

  async function loadTasks() {
    const res = await api.get<Task[]>("/Task");
    setTasks(res.data);
  }

  async function getById() {
    if (!searchId) return;
    const res = await api.get<Task>(`/Task/${searchId}`);
    setTasks([res.data]);
    setShowList(true);
  }

  async function handleCreateOrUpdate() {
    if (!form.title || !form.userId) return;

    setLoading(true);
    try {
      if (editingId) {
        await api.put(`/Task/${editingId}`, {
          ...form,
          priority: priorityMap.indexOf(form.priority),
        });
      } else {
        await api.post("/Task", {
          ...form,
          priority: priorityMap.indexOf(form.priority),
        });
      }

      resetForm();
      await loadTasks();
    } finally {
      setLoading(false);
    }
  }

  async function handleDelete(id: number) {
    if (!confirm("Tem certeza?")) return;
    await api.delete(`/Task/${id}`);
    loadTasks();
  }

  function handleEdit(task: Task) {
    setEditingId(task.id);
    setForm({
      title: task.title,
      description: task.description,
      priority: priorityMap[task.priority] as TaskForm["priority"],
      dueDate: "",
      userId: task.userId,
    });
  }

  function resetForm() {
    setEditingId(null);
    setForm({
      title: "",
      description: "",
      priority: "Medium",
      dueDate: "",
      userId: 0,
    });
  }

  return (
    <div style={{ width: "100%" }}>
      <h2 style={{ textAlign: "center" }}>Tarefas</h2>

      {/* FORM */}
      <div style={{ display: "flex", flexDirection: "column", gap: 12, background: "#1a1a2e", padding: 20, borderRadius: 12, marginBottom: 20 }}>
        <input placeholder="Título" value={form.title}
          onChange={(e) => setForm({ ...form, title: e.target.value })} />

        <input placeholder="Descrição" value={form.description}
          onChange={(e) => setForm({ ...form, description: e.target.value })} />

        <select value={form.priority}
          onChange={(e) => setForm({ ...form, priority: e.target.value as TaskForm["priority"] })}>
          <option value="Low">Low</option>
          <option value="Medium">Medium</option>
          <option value="High">High</option>
        </select>

        <input type="date" value={form.dueDate}
          onChange={(e) => setForm({ ...form, dueDate: e.target.value })} />

        <select value={form.userId}
          onChange={(e) => setForm({ ...form, userId: Number(e.target.value) })}>
          <option value={0}>Selecione usuário</option>
          {users.map((u) => (
            <option key={u.id} value={u.id}>{u.name}</option>
          ))}
        </select>

        <div style={{ display: "flex", gap: 10 }}>
          <button onClick={handleCreateOrUpdate} disabled={loading}>
            {editingId ? "Atualizar" : "Criar"}
          </button>

          <button onClick={loadTasks}>Listar</button>

          <button onClick={resetForm}>Limpar</button>
        </div>

        {/* BUSCA POR ID */}
        <div style={{ display: "flex", gap: 10 }}>
          <input
            placeholder="Buscar por ID"
            value={searchId}
            onChange={(e) => setSearchId(e.target.value)}
          />
          <button onClick={getById}>Buscar</button>
        </div>
      </div>

      {/* LISTA */}
      {tasks.length > 0 && (
        <div style={{ display: "flex", flexDirection: "column", gap: 12 }}>
          {tasks.map((t) => (
            <div key={t.id} style={{ background: "#1a1a2e", padding: 15, borderRadius: 12 }}>
              <div><b>Título:</b> {t.title}</div>
              <div><b>Descrição:</b> {t.description}</div>
              <div><b>Prioridade:</b> {priorityMap[t.priority]}</div>
              <div><b>UserId:</b> {t.userId}</div>

              <div style={{ display: "flex", gap: 10, marginTop: 10 }}>
                <button onClick={() => handleEdit(t)}>Editar</button>
                <button onClick={() => handleDelete(t.id)}>Deletar</button>
              </div>
            </div>
          ))}
        </div>
      )}
    </div>
  );
}