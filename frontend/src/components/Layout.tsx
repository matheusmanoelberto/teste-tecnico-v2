import { Link, useLocation } from 'react-router-dom';
import { Home, Users, Tags, ArrowRightLeft, FileBarChart } from 'lucide-react';
import { ReactNode } from 'react';

export default function Layout({ children }: { children: ReactNode }) {
  const location = useLocation();

  const links = [
    { name: 'Início', path: '/', icon: <Home className="w-5 h-5" /> },
    { name: 'Pessoas', path: '/pessoas', icon: <Users className="w-5 h-5" /> },
    { name: 'Categorias', path: '/categorias', icon: <Tags className="w-5 h-5" /> },
    { name: 'Transações', path: '/transacoes', icon: <ArrowRightLeft className="w-5 h-5" /> },
    { name: 'Relatórios', path: '/relatorios', icon: <FileBarChart className="w-5 h-5" /> },
  ];

  return (
    <div className="flex flex-col md:flex-row h-screen bg-neutral-900 text-slate-100">
      <aside className="w-full md:w-64 bg-neutral-950 p-4 md:p-6 flex flex-row md:flex-col border-b md:border-b-0 md:border-r border-neutral-800 shrink-0 overflow-x-auto">
        <h1 className="text-xl font-bold mb-0 md:mb-8 text-blue-400 hidden md:block">FinancialControl</h1>
        
        <h1 className="text-lg font-bold mb-0 mr-4 text-blue-400 block md:hidden flex-shrink-0 self-center">FC</h1>

        <nav className="flex flex-row md:flex-col gap-2 md:space-y-2 flex-nowrap w-full">
          {links.map((link) => {
            const isActive = location.pathname === link.path;
            return (
              <Link
                key={link.path}
                to={link.path}
                className={`flex items-center justify-center md:justify-start gap-2 md:gap-3 p-2 md:p-3 rounded-lg transition-colors whitespace-nowrap ${
                  isActive ? 'bg-blue-600 text-white' : 'hover:bg-neutral-800 text-neutral-400'
                }`}
              >
                {link.icon}
                <span className="font-medium hidden sm:block md:block">{link.name}</span>
              </Link>
            );
          })}
        </nav>
      </aside>
      <main className="flex-1 overflow-y-auto p-4 md:p-8">
        {children}
      </main>
    </div>
  );
}
