import api from "./api";

export const categoriasService = {
  getCategorias: async () => {
    const { data } = await api.get("/categorias");
    return data;
  },

  createCategoria: async (categoriaData: {
    descricao: string;
    finalidade: number;
  }) => {
    const { data } = await api.post("/categorias", categoriaData);
    return data;
  },

  deleteCategoria: async (id: number) => {
    await api.delete(`/categorias/${id}`);
  },
};
