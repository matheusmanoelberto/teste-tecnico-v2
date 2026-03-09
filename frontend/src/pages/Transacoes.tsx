import { useEffect, useState } from "react";
import { useCategorias } from "../hooks/useCategorias";
import api from "../services/api";
import { AxiosError } from "axios";

export default function Transacoes() {
  const { categorias } = useCategorias();

  const [transacoes, setTransacoes] = useState<any[]>([]);
  const [pessoas, setPessoas] = useState<any[]>([]);

  const [descricao, setDescricao] = useState("");
  const [valor, setValor] = useState("");
  const [tipo, setTipo] = useState("2");
  const [categoriaId, setCategoriaId] = useState("");
  const [pessoaId, setPessoaId] = useState("");

  const carregarDados = () => {
    api
      .get("/transacoes")
      .then((res) => setTransacoes(res.data))
      .catch(console.error);
    api
      .get("/pessoas")
      .then((res) => {
        setPessoas(res.data);
        if (res.data.length > 0 && !pessoaId)
          setPessoaId(res.data[0].id.toString());
      })
      .catch(console.error);
  };

  useEffect(() => {
    carregarDados();
  }, []);

  useEffect(() => {
    if (categorias.length > 0 && !categoriaId) {
      setCategoriaId(categorias[0].id.toString());
    }
  }, [categorias, categoriaId]);

  const handleCadastrar = async (e: React.FormEvent) => {
    e.preventDefault();

    const pessoaSelecionada = pessoas.find((p) => p.id.toString() === pessoaId);
    if (pessoaSelecionada && pessoaSelecionada.id < 18 && tipo === "1") {
      alert(
        "Menores de 18 anos só podem registrar transações do tipo Despesa.",
      );
      return;
    }

    const categoriaSelecionada = categorias.find(
      (c) => c.id.toString() === categoriaId,
    );
    if (categoriaSelecionada) {
      if (
        (tipo === "1" && categoriaSelecionada.finalidade === 2) ||
        (tipo === "2" && categoriaSelecionada.finalidade === 1)
      ) {
        alert(
          "Esta categoria não é compatível com o tipo de transação selecionado.",
        );
        return;
      }
    }

    try {
      await api.post("/transacoes", {
        descricao,
        valor: parseFloat(valor),
        tipo: parseInt(tipo),
        categoriaId: parseInt(categoriaId),
        pessoaId: parseInt(pessoaId),
      });
      setDescricao("");
      setValor("");
      carregarDados();
    } catch (error: unknown) {
      const err = error as AxiosError<string>;
      alert(err.response?.data || "Erro ao registrar transação.");
    }
  };

  const handleExcluir = async (id: number) => {
    if (!window.confirm("Tem certeza que deseja excluir esta transação?"))
      return;
    try {
      await api.delete(`/transacoes/${id}`);
      carregarDados();
    } catch (error: unknown) {
      const err = error as AxiosError<{ error: string }>;
      alert(err.response?.data?.error || "Erro ao excluir transação.");
    }
  };

  return (
    <div>
      <h1 className="text-3xl font-bold mb-6">Transações</h1>

      <div className="bg-neutral-800 rounded-lg p-6 border border-neutral-700 mb-6">
        <h2 className="text-xl font-semibold mb-4 text-blue-400">
          Nova Transação
        </h2>
        <form
          onSubmit={handleCadastrar}
          className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-6 gap-4 items-end"
        >
          <div className="lg:col-span-2">
            <label
              htmlFor="descricao"
              className="block text-sm font-medium text-neutral-400 mb-1"
            >
              Descrição
            </label>
            <input
              id="descricao"
              type="text"
              value={descricao}
              onChange={(e) => setDescricao(e.target.value)}
              className="w-full bg-neutral-900 border border-neutral-700 rounded-lg px-4 py-2 text-white"
              required
            />
          </div>

          <div>
            <label
              htmlFor="valor"
              className="block text-sm font-medium text-neutral-400 mb-1"
            >
              Valor (R$)
            </label>
            <input
              id="valor"
              type="number"
              step="0.01"
              value={valor}
              onChange={(e) => setValor(e.target.value)}
              className="w-full bg-neutral-900 border border-neutral-700 rounded-lg px-4 py-2 text-white"
              required
            />
          </div>

          <div>
            <label
              htmlFor="tipo"
              className="block text-sm font-medium text-neutral-400 mb-1"
            >
              Tipo
            </label>
            <select
              id="tipo"
              value={tipo}
              onChange={(e) => setTipo(e.target.value)}
              className="w-full bg-neutral-900 border border-neutral-700 rounded-lg px-4 py-2 text-white"
            >
              <option value="1">Receita</option>
              <option value="2">Despesa</option>
            </select>
          </div>

          <div>
            <label
              htmlFor="categoria"
              className="block text-sm font-medium text-neutral-400 mb-1"
            >
              Categoria
            </label>
            <select
              id="categoria"
              value={categoriaId}
              onChange={(e) => setCategoriaId(e.target.value)}
              className="w-full bg-neutral-900 border border-neutral-700 rounded-lg px-4 py-2 text-white"
              required
            >
              {categorias
                .filter(
                  (c) => c.finalidade === 3 || c.finalidade.toString() === tipo,
                )
                .map((c) => (
                  <option key={c.id} value={c.id}>
                    {c.descricao}
                  </option>
                ))}
            </select>
          </div>

          <div>
            <label
              htmlFor="pessoa"
              className="block text-sm font-medium text-neutral-400 mb-1"
            >
              Pessoa
            </label>
            <select
              id="pessoa"
              value={pessoaId}
              onChange={(e) => setPessoaId(e.target.value)}
              className="w-full bg-neutral-900 border border-neutral-700 rounded-lg px-4 py-2 text-white"
              required
            >
              {pessoas.map((p) => (
                <option key={p.id} value={p.id}>
                  {p.nome}
                </option>
              ))}
            </select>
          </div>

          <div className="lg:col-span-6 flex justify-end">
            <button
              type="submit"
              className="bg-blue-600 hover:bg-blue-700 text-white font-medium py-2 px-6 rounded-lg"
            >
              Registrar
            </button>
          </div>
        </form>
      </div>

      <div className="bg-neutral-800 rounded-lg p-6 border border-neutral-700">
        <ul className="space-y-4">
          {transacoes.length === 0 && (
            <p className="text-neutral-400">Nenhuma transação registrada.</p>
          )}
          {transacoes.map((t) => (
            <li
              key={t.id}
              className="flex justify-between items-center py-2 border-b border-neutral-700 last:border-0"
            >
              <span className="font-medium text-white">{t.descricao}</span>
              <div className="flex items-center gap-4">
                <span
                  className={`font-bold ${t.tipo === 1 ? "text-green-500" : "text-red-500"}`}
                >
                  {t.tipo === 1 ? "+" : "-"} R$ {t.valor.toFixed(2)}
                </span>
                <button
                  onClick={() => handleExcluir(t.id)}
                  className="text-red-500 hover:text-red-400 text-sm font-medium transition-colors"
                >
                  Excluir
                </button>
              </div>
            </li>
          ))}
        </ul>
      </div>
    </div>
  );
}
