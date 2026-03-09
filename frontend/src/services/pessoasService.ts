import api from "./api";
import { Pessoa } from "../types/Pessoa.type";

export const pessoasService = {
  getPessoas: async (): Promise<Pessoa[]> => {
    const { data } = await api.get("/pessoas");
    return data;
  },

  createPessoa: async (pessoaData: Omit<Pessoa, "id">): Promise<Pessoa> => {
    const { data } = await api.post("/pessoas", pessoaData);
    return data;
  },

  deletePessoa: async (id: number): Promise<void> => {
    await api.delete(`/pessoas/${id}`);
  },
};
