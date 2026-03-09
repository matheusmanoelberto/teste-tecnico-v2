import { useState } from "react";
import { useCategorias } from "../hooks/useCategorias";
import { AxiosError } from "axios";

export default function Categorias() {
  const { categorias, cadastrar, excluir, loading } = useCategorias();
  const [descricao, setDescricao] = useState("");
  const [finalidade, setFinalidade] = useState("3");

  const onSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    try {
      await cadastrar(descricao, finalidade);
      setDescricao("");
      setFinalidade("3");
    } catch (error: unknown) {
      const err = error as AxiosError<string>;

      const mensagem = err.response?.data || "Erro ao cadastrar categoria.";
      alert(mensagem);
    }
  };

  return (
    <div>
      <h1 className="text-3xl font-bold mb-6">Categorias</h1>

      {/* Formulário */}
      <div className="bg-neutral-800 rounded-lg p-4 md:p-6 border border-neutral-700 mb-6">
        <h2 className="text-xl font-semibold mb-4 text-blue-400">
          Nova Categoria
        </h2>
        <form
          onSubmit={onSubmit}
          className="flex flex-col md:flex-row gap-4 items-stretch md:items-end"
        >
          <div className="flex-1">
            <label className="block text-sm font-medium text-neutral-400 mb-1">
              Descrição
            </label>
            <input
              type="text"
              value={descricao}
              onChange={(e) => setDescricao(e.target.value)}
              className="w-full bg-neutral-900 border border-neutral-700 rounded-lg px-4 py-2 focus:outline-none focus:border-blue-500"
              required
            />
          </div>
          <div className="w-full md:w-48">
            <label className="block text-sm font-medium text-neutral-400 mb-1">
              Finalidade
            </label>
            <select
              value={finalidade}
              onChange={(e) => setFinalidade(e.target.value)}
              className="w-full bg-neutral-900 border border-neutral-700 rounded-lg px-4 py-2 focus:outline-none focus:border-blue-500"
            >
              <option value="1">Receita</option>
              <option value="2">Despesa</option>
              <option value="3">Ambas</option>
            </select>
          </div>
          <button
            type="submit"
            className="w-full md:w-auto bg-blue-600 hover:bg-blue-700 text-white font-medium py-2 px-6 rounded-lg"
          >
            Cadastrar
          </button>
        </form>
      </div>

      {/* Lista */}
      <div className="bg-neutral-800 rounded-lg p-6 border border-neutral-700">
        {loading ? (
          <p>Carregando...</p>
        ) : (
          <ul className="space-y-4">
            {categorias.length === 0 && (
              <p className="text-neutral-400">Nenhuma categoria.</p>
            )}
            {categorias.map((c) => (
              <li
                key={c.id}
                className="flex justify-between items-center py-2 border-b border-neutral-700 last:border-0"
              >
                <span className="font-medium">{c.descricao}</span>
                <div className="flex items-center gap-4">
                  <span className="text-sm bg-blue-600/20 text-blue-400 px-3 py-1 rounded-full">
                    {c.finalidade === 1
                      ? "Receita"
                      : c.finalidade === 2
                        ? "Despesa"
                        : "Ambas"}
                  </span>
                  <button
                    onClick={() => excluir(c.id)}
                    className="text-red-500 hover:text-red-400 text-sm font-medium"
                  >
                    Excluir
                  </button>
                </div>
              </li>
            ))}
          </ul>
        )}
      </div>
    </div>
  );
}
