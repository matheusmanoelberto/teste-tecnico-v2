import { useEffect, useState } from 'react';
import api from '../services/api';

export default function Relatorios() {
  const [totaisPessoa, setTotaisPessoa] = useState<any[]>([]);
  const [totalGeral, setTotalGeral] = useState<any>(null);

  useEffect(() => {
    api.get('/relatorios/pessoas').then(res => setTotaisPessoa(res.data)).catch(console.error);
    api.get('/relatorios/geral').then(res => setTotalGeral(res.data)).catch(console.error);
  }, []);

  return (
    <div>
      <h1 className="text-3xl font-bold mb-6">Relatórios</h1>
      
      {totalGeral && (
        <div className="grid grid-cols-1 md:grid-cols-3 gap-4 md:gap-6 mb-8">
          <div className="bg-neutral-800 p-4 md:p-6 rounded-xl border border-neutral-700">
            <h2 className="text-neutral-400 text-sm mb-2">A Receber</h2>
            <p className="text-2xl md:text-3xl font-bold text-green-500">R$ {totalGeral.totalGeraLReceitas.toFixed(2)}</p>
          </div>
          <div className="bg-neutral-800 p-4 md:p-6 rounded-xl border border-neutral-700">
            <h2 className="text-neutral-400 text-sm mb-2">A Pagar</h2>
            <p className="text-2xl md:text-3xl font-bold text-red-500">R$ {totalGeral.totalGeralDespesas.toFixed(2)}</p>
          </div>
          <div className="bg-neutral-800 p-4 md:p-6 rounded-xl border border-neutral-700">
            <h2 className="text-neutral-400 text-sm mb-2">Saldo Liquido</h2>
            <p className={`text-2xl md:text-3xl font-bold ${totalGeral.saldoLiquido >= 0 ? 'text-green-500' : 'text-red-500'}`}>
              R$ {totalGeral.saldoLiquido.toFixed(2)}
            </p>
          </div>
        </div>
      )}

      <h2 className="text-2xl font-semibold mb-4 text-blue-400">Por Pessoa</h2>
      <div className="bg-neutral-800 rounded-lg p-4 md:p-6 border border-neutral-700 overflow-x-auto">
        <table className="w-full text-left border-collapse min-w-[600px]">
          <thead>
            <tr className="border-b border-neutral-700 text-neutral-400 text-sm">
              <th className="pb-3 font-medium">Nome</th>
              <th className="pb-3 font-medium">Receitas</th>
              <th className="pb-3 font-medium">Despesas</th>
              <th className="pb-3 font-medium">Saldo</th>
            </tr>
          </thead>
          <tbody>
            {totaisPessoa.map((p, idx) => (
              <tr key={idx} className="border-b border-neutral-700/50 last:border-0 hover:bg-neutral-700/20 transition-colors">
                <td className="py-4 font-semibold text-lg">{p.nomePessoa}</td>
                <td className="py-4 text-green-500">R$ {p.totalReceitas.toFixed(2)}</td>
                <td className="py-4 text-red-500">R$ {p.totalDespesas.toFixed(2)}</td>
                <td className={`py-4 font-bold ${p.saldo >= 0 ? 'text-green-500' : 'text-red-500'}`}>
                  R$ {p.saldo.toFixed(2)}
                </td>
              </tr>
            ))}
            {totaisPessoa.length === 0 && (
              <tr>
                <td colSpan={4} className="py-6 text-center text-neutral-500">
                  Nenhuma transação encontrada para gerar relatório.
                </td>
              </tr>
            )}
          </tbody>
        </table>
      </div>
    </div>
  );
}
