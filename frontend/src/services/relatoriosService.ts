import api from "./api";
import { RelatorioPessoa } from "../types/RelatorioPessoa.type";
import { RelatorioGeral } from "../types/RelatorioGeral.type";

export const relatoriosService = {
  getTotaisPessoa: async (): Promise<RelatorioPessoa[]> => {
    const { data } = await api.get("/relatorios/pessoas");
    return data;
  },

  getTotalGeral: async (): Promise<RelatorioGeral> => {
    const { data } = await api.get("/relatorios/geral");
    return data;
  },
};
