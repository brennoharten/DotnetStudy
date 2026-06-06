import type { Metadata } from 'next';
import './globals.css';

export const metadata: Metadata = {
  title: 'Landing Pages',
  description: 'Landing pages dinâmicas'
};

export default function RootLayout({ children }: { children: React.ReactNode }) {
  return (
    <html lang="pt-BR">
      <body>{children}</body>
    </html>
  );
}
