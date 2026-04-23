import { useEffect, useState } from "react";
import { api } from "../services/api";

type User = {
  id: number;
  name: string;
  email: string;
};

export function Users() {
  const [users, setUsers] = useState<User[]>([]);
  const [selectedUser, setSelectedUser] = useState<User | null>(null);
  const [showList, setShowList] = useState(false);
  const [searchId, setSearchId] = useState("");

  const [form, setForm] = useState({
    name: "",
    email: "",
  });

  async function load() {
    const res = await api.get<User[]>("/User");
    setUsers(res.data);
  }

  async function handleCreate() {
    if (!form.name || !form.email) return;

    await api.post("/User", form);

    setForm({ name: "", email: "" });
  }

  async function handleSearch() {
    if (!searchId) return;

    const res = await api.get<User>(`/User/${searchId}`);
    setSelectedUser(res.data);
    setShowList(false);
  }

  async function handleDelete(id: number) {
    if (!confirm("Tem certeza?")) return;

    await api.delete(`/User/${id}`);
    await load();
  }

  async function handleUpdate(user: User) {
    const newName = prompt("Novo nome:", user.name);
    const newEmail = prompt("Novo email:", user.email);

    if (!newName || !newEmail) return;

    await api.put(`/User/${user.id}`, {
      name: newName,
      email: newEmail,
    });

    await load();
  }

  function handleList() {
    load();
    setShowList(true);
    setSelectedUser(null);
  }

  function handleClear() {
    setShowList(false);
    setSelectedUser(null);
    setUsers([]);
  }

  return (
    <div style={{ width: "100%" }}>
      <h2 style={{ textAlign: "center", marginBottom: 20 }}>
        Usuários
      </h2>

      {/* FORM */}
      <div
        style={{
          display: "flex",
          flexDirection: "column",
          gap: 10,
          background: "#1a1a2e",
          padding: 20,
          borderRadius: 12,
          marginBottom: 20,
        }}
      >
        <input
          placeholder="Nome"
          value={form.name}
          onChange={(e) => setForm({ ...form, name: e.target.value })}
        />

        <input
          placeholder="Email"
          value={form.email}
          onChange={(e) => setForm({ ...form, email: e.target.value })}
        />

        {/* BOTÕES */}
        <div style={{ display: "flex", gap: 10 }}>
          <button onClick={handleCreate}>Criar</button>
          <button onClick={handleList}>Listar</button>
          <button onClick={handleClear}>Limpar</button>
        </div>

        {/* BUSCAR POR ID */}
        <div style={{ display: "flex", gap: 10 }}>
          <input
            placeholder="Buscar por ID"
            value={searchId}
            onChange={(e) => setSearchId(e.target.value)}
          />
          <button onClick={handleSearch}>Buscar</button>
        </div>
      </div>

      {/* RESULTADO POR ID */}
      {selectedUser && (
        <div
          style={{
            background: "#1a1a2e",
            padding: 20,
            borderRadius: 12,
            marginBottom: 15,
          }}
        >
          <p><b>Id:</b> {selectedUser.id}</p>
          <p><b>Nome:</b> {selectedUser.name}</p>
          <p><b>Email:</b> {selectedUser.email}</p>

          <div style={{ display: "flex", gap: 10 }}>
            <button onClick={() => handleUpdate(selectedUser)}>Editar</button>
            <button onClick={() => handleDelete(selectedUser.id)}>Deletar</button>
          </div>
        </div>
      )}

      {/* LISTA */}
      {showList && (
        <div style={{ display: "flex", flexDirection: "column", gap: 10 }}>
          {users.map((u) => (
            <div
              key={u.id}
              style={{
                background: "#1a1a2e",
                padding: 15,
                borderRadius: 10,
              }}
            >
              <p><b>Id:</b> {u.id}</p>
              <p><b>Nome:</b> {u.name}</p>
              <p><b>Email:</b> {u.email}</p>

              <div style={{ display: "flex", gap: 10 }}>
                <button onClick={() => handleUpdate(u)}>Editar</button>
                <button onClick={() => handleDelete(u.id)}>Deletar</button>
              </div>
            </div>
          ))}
        </div>
      )}
    </div>
  );
}