import { useState } from "react";
import { Users } from "./pages/Users";
import { Tasks } from "./pages/Tasks";
import { Dashboard } from "./pages/Dashboard";

type Tab = "tasks" | "users" | "overview";

function App() {
  const [tab, setTab] = useState<Tab>("tasks");

  return (
    <div
      style={{
        paddingTop: 60,
        fontFamily: "Inter, sans-serif",
        minHeight: "100vh",
        display: "flex",
        flexDirection: "column",
        alignItems: "center",
      }}
    >
      {/* TÍTULO */}
      <h1
        style={{
          marginBottom: 30,
          fontSize: 42,
          fontWeight: 700,
        }}
      >
        Task Manager
      </h1>

      {/* 🔥 CONTAINER FIXO DA NAVBAR */}
      <div
        style={{
          width: 600, // 👈 largura FIXA (NUNCA MUDA)
          display: "flex",
          justifyContent: "center",
          marginBottom: 40,
        }}
      >
        <div
          style={{
            display: "flex",
            gap: 10,
            padding: 12,
            borderRadius: 999,
            background: "linear-gradient(135deg, #1e1e2f, #2c2c54)",
            boxShadow: "0 4px 12px rgba(0,0,0,0.2)",
            width: "100%", // ocupa os 600 fixos
          }}
        >
          {[
            { key: "tasks", label: "Tasks" },
            { key: "users", label: "Users" },
            { key: "overview", label: "Overview" },
          ].map((item) => (
            <button
              key={item.key}
              onClick={() => setTab(item.key as Tab)}
              style={{
                flex: 1,
                padding: "10px 0",
                borderRadius: 999,
                border: "none",
                cursor: "pointer",
                fontWeight: 500,
                background:
                  tab === item.key
                    ? "#4f46e5"
                    : "rgba(255,255,255,0.1)",
                color: "#fff",
                transition: "all 0.2s ease",
              }}
            >
              {item.label}
            </button>
          ))}
        </div>
      </div>

      {/* 🔥 CONTEÚDO SEPARADO */}
      <div
        style={{
          width: 600, // 👈 MESMA LARGURA DA NAVBAR
        }}
      >
        {tab === "tasks" && <Tasks />}
        {tab === "users" && <Users />}
        {tab === "overview" && <Dashboard />}
      </div>
    </div>
  );
}

export default App;