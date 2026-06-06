import { notFound } from 'next/navigation';
import type { PublicLandingPage } from '@/types/landing-page';

const apiBaseUrl = process.env.NEXT_PUBLIC_API_BASE_URL ?? 'http://localhost:5137';

export async function getLandingPage(slug: string): Promise<PublicLandingPage> {
  const response = await fetch(`${apiBaseUrl}/api/public/landing-pages/${encodeURIComponent(slug)}`, {
    cache: 'no-store'
  });

  if (response.status === 404) {
    notFound();
  }

  if (!response.ok) {
    throw new Error('Falha ao carregar a landing page.');
  }

  return response.json();
}
