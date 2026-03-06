import { Link } from 'react-router-dom';

export default function Home() {
  return (
    <div className="flex flex-col gap-4 max-w-2xl">
      <h1 className="text-3xl font-bold">Bem vindo ao Financial Control</h1>
      <p className="text-neutral-400 text-lg">
        Este é um sistema de controle de gastos residenciais onde você pode gerenciar as pessoas da casa, as categorias de gastos, registrar receitas e despesas e visualizar relatórios consolidados.
      </p>
      
      <div className="grid grid-cols-1 md:grid-cols-2 gap-4 mt-8">
        <Link to="/pessoas" className="bg-neutral-800 p-6 rounded-xl border border-neutral-700 hover:border-blue-500 hover:bg-neutral-800/80 transition-all cursor-pointer block">
          <h2 className="text-xl font-semibold mb-2">Pessoas</h2>
          <p className="text-neutral-400 text-sm">Gerencie os moradores e perfis associados.</p>
        </Link>
        <Link to="/categorias" className="bg-neutral-800 p-6 rounded-xl border border-neutral-700 hover:border-blue-500 hover:bg-neutral-800/80 transition-all cursor-pointer block">
          <h2 className="text-xl font-semibold mb-2">Categorias</h2>
          <p className="text-neutral-400 text-sm">Cadastre e liste as naturezas dos seus gastos e ganhos.</p>
        </Link>
        <Link to="/transacoes" className="bg-neutral-800 p-6 rounded-xl border border-neutral-700 hover:border-blue-500 hover:bg-neutral-800/80 transition-all cursor-pointer block">
          <h2 className="text-xl font-semibold mb-2">Transações</h2>
          <p className="text-neutral-400 text-sm">Registre e acompanhe as receitas e despesas da casa.</p>
        </Link>
        <Link to="/relatorios" className="bg-neutral-800 p-6 rounded-xl border border-neutral-700 hover:border-blue-500 hover:bg-neutral-800/80 transition-all cursor-pointer block">
          <h2 className="text-xl font-semibold mb-2">Relatórios</h2>
          <p className="text-neutral-400 text-sm">Visualize o consolidade e o saldo disponível de cada pessoa e categoria.</p>
        </Link>
      </div>
    </div>
  );
}
