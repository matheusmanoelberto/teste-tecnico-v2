import { useState, useEffect, useCallback } from "react";
import { pessoasService } from "../services/pessoasService";
import { Pessoa } from "../types/Pessoa.type";

export const usePessoas = () => {
  const [pessoas, setPessoas] = useState<Pessoa[]>([]);
  const [loading, setLoading] = useState(false);

  const carregarPessoas = useCallback(async () => {
    setLoading(true);
    try {
      const data = await pessoasService.getPessoas();
      setPessoas(data);
    } catch (err) {
      console.error("Falha ao carregar pessoas:", err);
    } finally {
      setLoading(false);
    }
  }, []);

  const cadastrarPessoa = async (nome: string, idade: number) => {
    await pessoasService.createPessoa({ nome, idade });
    await carregarPessoas();
  };

  const excluirPessoa = async (id: number) => {
    if (!globalThis.confirm("Tem certeza que deseja excluir esta pessoa?"))
      return;
    await pessoasService.deletePessoa(id);
    await carregarPessoas();
  };

  useEffect(() => {
    carregarPessoas();
  }, [carregarPessoas]);

  return { pessoas, loading, carregarPessoas, cadastrarPessoa, excluirPessoa };
};
