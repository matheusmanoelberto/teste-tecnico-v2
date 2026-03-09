import { useState, useCallback, useEffect } from "react";
import { transacoesService } from "../services/transacoesService";
import { pessoasService } from "../services/pessoasService";
import { Transacao } from "../types/Transacao.type";
import { Pessoa } from "../types/Pessoa.type";

export const useTransacoes = () => {
  const [transacoes, setTransacoes] = useState<Transacao[]>([]);
  const [pessoas, setPessoas] = useState<Pessoa[]>([]);
  const [loading, setLoading] = useState(false);

  const carregarDados = useCallback(async () => {
    setLoading(true);
    try {
      const [transacoesData, pessoasData] = await Promise.all([
        transacoesService.getTransacoes(),
        pessoasService.getPessoas(),
      ]);
      setTransacoes(transacoesData);
      setPessoas(pessoasData);
    } catch (err) {
      console.error("Falha ao carregar transações/pessoas:", err);
    } finally {
      setLoading(false);
    }
  }, []);

  useEffect(() => {
    carregarDados();
  }, [carregarDados]);

  const cadastrarTransacao = async (
    descricao: string,
    valor: number,
    tipo: number,
    categoriaId: number,
    pessoaId: number
  ) => {
    await transacoesService.createTransacao({
      descricao,
      valor,
      tipo,
      categoriaId,
      pessoaId,
    });
    await carregarDados();
  };

  const excluirTransacao = async (id: number) => {
    if (!window.confirm("Tem certeza que deseja excluir esta transação?")) return;
    await transacoesService.deleteTransacao(id);
    await carregarDados();
  };

  return { 
    transacoes, 
    pessoas, 
    loading, 
    carregarDados, 
    cadastrarTransacao, 
    excluirTransacao 
  };
};
