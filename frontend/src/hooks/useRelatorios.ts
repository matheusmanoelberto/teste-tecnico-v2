import { useState, useEffect, useCallback } from "react";
import { relatoriosService } from "../services/relatoriosService";
import { RelatorioPessoa } from "../types/RelatorioPessoa.type";
import { RelatorioGeral } from "../types/RelatorioGeral.type";

export const useRelatorios = () => {
  const [totaisPessoa, setTotaisPessoa] = useState<RelatorioPessoa[]>([]);
  const [totalGeral, setTotalGeral] = useState<RelatorioGeral | null>(null);
  const [loading, setLoading] = useState(false);

  const carregar = useCallback(async () => {
    setLoading(true);
    try {
      const [pessoasData, geralData] = await Promise.all([
        relatoriosService.getTotaisPessoa(),
        relatoriosService.getTotalGeral()
      ]);
      setTotaisPessoa(pessoasData);
      setTotalGeral(geralData);
    } catch (err) {
      console.error("Falha ao carregar relatórios:", err);
    } finally {
      setLoading(false);
    }
  }, []);

  useEffect(() => {
    carregar();
  }, [carregar]);

  return { totaisPessoa, totalGeral, loading, carregar };
};
