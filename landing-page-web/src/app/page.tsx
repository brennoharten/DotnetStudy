import Link from 'next/link';

export default function Home() {
  return (
    <main className="flex min-h-screen items-center justify-center bg-slate-950 px-6 text-center text-white">
      <div>
        <h1 className="text-4xl font-bold">Landing Pages</h1>
        <p className="mt-4 text-slate-300">Acesse uma landing page pública pelo slug.</p>
        <Link className="mt-6 inline-flex rounded-full bg-cyan-400 px-5 py-3 font-semibold text-slate-950" href="/academia-fit-pro">
          Abrir exemplo
        </Link>
      </div>
    </main>
  );
}
