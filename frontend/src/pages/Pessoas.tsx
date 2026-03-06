import { useEffect, useState } from 'react';
import api from '../services/api';

export default function Pessoas() {
  const [pessoas, setPessoas] = useState<any[]>([]);
  const [nome, setNome] = useState('');
  const [idade, setIdade] = useState('');

  const carregarPessoas = () => {
    api.get('/pessoas').then(res => setPessoas(res.data)).catch(console.error);
  };

  useEffect(() => {
    carregarPessoas();
  }, []);

  const handleCadastrar = async (e: React.FormEvent) => {
    e.preventDefault();
    try {
      await api.post('/pessoas', { nome, idade: parseInt(idade) });
      setNome('');
      setIdade('');
      carregarPessoas();
    } catch (error: any) {
      alert(error.response?.data || 'Erro ao cadastrar pessoa.');
    }
  };

  const handleExcluir = async (id: number) => {
    if (!window.confirm('Tem certeza que deseja excluir esta pessoa?')) return;
    try {
      await api.delete(`/pessoas/${id}`);
      carregarPessoas();
    } catch (error: any) {
      alert(error.response?.data?.error || 'Erro ao excluir pessoa.');
    }
  };

  return (
    <div>
      <h1 className="text-3xl font-bold mb-6">Pessoas</h1>
      
      <div className="bg-neutral-800 rounded-lg p-4 md:p-6 border border-neutral-700 mb-6">
        <h2 className="text-xl font-semibold mb-4 text-blue-400">Nova Pessoa</h2>
        <form onSubmit={handleCadastrar} className="flex flex-col md:flex-row gap-4 items-stretch md:items-end">
          <div className="flex-1">
            <label className="block text-sm font-medium text-neutral-400 mb-1">Nome</label>
            <input 
              type="text" 
              value={nome} 
              onChange={e => setNome(e.target.value)} 
              className="w-full bg-neutral-900 border border-neutral-700 rounded-lg px-4 py-2 focus:outline-none focus:border-blue-500" 
              required 
              maxLength={200}
            />
          </div>
          <div className="w-full md:w-32">
            <label className="block text-sm font-medium text-neutral-400 mb-1">Idade</label>
            <input 
              type="number" 
              value={idade} 
              onChange={e => setIdade(e.target.value)} 
              className="w-full bg-neutral-900 border border-neutral-700 rounded-lg px-4 py-2 focus:outline-none focus:border-blue-500" 
              required 
              min="0"
            />
          </div>
          <button type="submit" className="w-full md:w-auto bg-blue-600 hover:bg-blue-700 text-white font-medium py-2 px-6 rounded-lg transition-colors">
            Cadastrar
          </button>
        </form>
      </div>

      <div className="bg-neutral-800 rounded-lg p-6 border border-neutral-700">
        <ul className="space-y-4">
          {pessoas.length === 0 && <p className="text-neutral-400">Nenhuma pessoa cadastrada.</p>}
          {pessoas.map(p => (
            <li key={p.id} className="flex justify-between items-center py-2 border-b border-neutral-700 last:border-0">
              <span className="font-medium text-lg">{p.nome}</span>
              <div className="flex items-center gap-4">
                <span className="text-neutral-400 bg-neutral-900 px-3 py-1 rounded-full text-sm">{p.idade} anos</span>
                <button 
                  onClick={() => handleExcluir(p.id)}
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
