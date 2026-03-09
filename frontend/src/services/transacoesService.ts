import api from "./api";
import { Transacao } from "../types/Transacao.type";

export const transacoesService = {
  getTransacoes: async (): Promise<Transacao[]> => {
    const { data } = await api.get("/transacoes");
    return data;
  },

  createTransacao: async (transacaoData: Omit<Transacao, "id">): Promise<Transacao> => {
    const { data } = await api.post("/transacoes", transacaoData);
    return data;
  },

  deleteTransacao: async (id: number): Promise<void> => {
    await api.delete(`/transacoes/${id}`);
  },
};
