import { useState, useEffect, useCallback } from "react";
import { categoriasService } from "../services/categoriasService";
import { Categoria } from "../types/Categoria";

export const useCategorias = () => {
  const [categorias, setCategorias] = useState<Categoria[]>([]);
  const [loading, setLoading] = useState(false);

  const carregar = useCallback(async () => {
    setLoading(true);
    try {
      const data = await categoriasService.getCategorias();
      setCategorias(data);
    } catch (err) {
      console.error("Falha ao carregar:", err);
    } finally {
      setLoading(false);
    }
  }, []);

  const cadastrar = async (descricao: string, finalidade: string) => {
    await categoriasService.createCategoria({
      descricao,
      finalidade: Number.parseInt(finalidade),
    });
    await carregar();
  };

  const excluir = async (id: number) => {
    if (!globalThis.confirm("Tem certeza que deseja excluir esta categoria?"))
      return;
    await categoriasService.deleteCategoria(id);
    await carregar();
  };

  useEffect(() => {
    carregar();
  }, [carregar]);

  return { categorias, loading, cadastrar, excluir };
};
