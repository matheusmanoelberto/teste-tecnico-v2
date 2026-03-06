import { useEffect, useState } from 'react';
import api from '../services/api';

export default function Categorias() {
  const [categorias, setCategorias] = useState<any[]>([]);
  const [descricao, setDescricao] = useState('');
  const [finalidade, setFinalidade] = useState('3'); // 3 = Ambas (default)

  const carregarCategorias = () => {
    api.get('/categorias').then(res => setCategorias(res.data)).catch(console.error);
  };

  useEffect(() => {
    carregarCategorias();
  }, []);

  const handleCadastrar = async (e: React.FormEvent) => {
    e.preventDefault();
    try {
      await api.post('/categorias', { descricao, finalidade: parseInt(finalidade) });
      setDescricao('');
      setFinalidade('3');
      carregarCategorias();
    } catch (error: any) {
      alert(error.response?.data || 'Erro ao cadastrar categoria.');
    }
  };

  const handleExcluir = async (id: number) => {
    if (!window.confirm('Tem certeza que deseja excluir esta categoria?')) return;
    try {
      await api.delete(`/categorias/${id}`);
      carregarCategorias();
    } catch (error: any) {
      alert(error.response?.data?.error || 'Erro ao excluir categoria.');
    }
  };

  return (
    <div>
      <h1 className="text-3xl font-bold mb-6">Categorias</h1>

      <div className="bg-neutral-800 rounded-lg p-4 md:p-6 border border-neutral-700 mb-6">
        <h2 className="text-xl font-semibold mb-4 text-blue-400">Nova Categoria</h2>
        <form onSubmit={handleCadastrar} className="flex flex-col md:flex-row gap-4 items-stretch md:items-end">
          <div className="flex-1">
            <label className="block text-sm font-medium text-neutral-400 mb-1">Descrição</label>
            <input 
              type="text" 
              value={descricao} 
              onChange={e => setDescricao(e.target.value)} 
              className="w-full bg-neutral-900 border border-neutral-700 rounded-lg px-4 py-2 focus:outline-none focus:border-blue-500" 
              required 
              maxLength={100}
            />
          </div>
          <div className="w-full md:w-48">
            <label className="block text-sm font-medium text-neutral-400 mb-1">Finalidade</label>
            <select 
              value={finalidade} 
              onChange={e => setFinalidade(e.target.value)} 
              className="w-full bg-neutral-900 border border-neutral-700 rounded-lg px-4 py-2 focus:outline-none focus:border-blue-500"
            >
              <option value="1">Receita</option>
              <option value="2">Despesa</option>
              <option value="3">Ambas</option>
            </select>
          </div>
          <button type="submit" className="w-full md:w-auto bg-blue-600 hover:bg-blue-700 text-white font-medium py-2 px-6 rounded-lg transition-colors">
            Cadastrar
          </button>
        </form>
      </div>

      <div className="bg-neutral-800 rounded-lg p-6 border border-neutral-700">
        <ul className="space-y-4">
          {categorias.length === 0 && <p className="text-neutral-400">Nenhuma categoria cadastrada.</p>}
          {categorias.map(c => (
            <li key={c.id} className="flex justify-between items-center py-2 border-b border-neutral-700 last:border-0">
              <span className="font-medium">{c.descricao}</span>
              <div className="flex items-center gap-4">
                <span className="text-sm bg-blue-600/20 text-blue-400 px-3 py-1 rounded-full">
                  {c.finalidade === 1 ? 'Receita' : c.finalidade === 2 ? 'Despesa' : 'Ambas'}
                </span>
                <button 
                  onClick={() => handleExcluir(c.id)}
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
