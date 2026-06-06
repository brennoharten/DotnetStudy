import type { Metadata } from 'next';
import { getLandingPage } from '@/lib/api';
import {
  CallToActionSection,
  ContactSection,
  FaqSection,
  FeaturesSection,
  GallerySection,
  HeroSection,
  LocationSection,
  OpeningHoursSection,
  PricingSection,
  SeoSection,
  ServicesSection,
  SocialLinksSection,
  TestimonialsSection
} from '@/components/landing/sections';

type PageProps = {
  params: { slug: string };
};

export async function generateMetadata({ params }: PageProps): Promise<Metadata> {
  const { slug } = params;
  const page = await getLandingPage(slug);

  return {
    title: page.seo?.title || page.title,
    description: page.seo?.description || page.subtitle,
    keywords: page.seo?.keywords,
    openGraph: {
      title: page.seo?.title || page.title,
      description: page.seo?.description || page.subtitle,
      images: page.heroImageUrl ? [page.heroImageUrl] : undefined,
      url: `/${page.slug}`
    },
    alternates: {
      canonical: `/${page.slug}`
    }
  };
}

export default async function LandingPage({ params }: PageProps) {
  const { slug } = params;
  const page = await getLandingPage(slug);

  return (
    <main className="min-h-screen bg-slate-950 text-white">
      <HeroSection page={page} />
      <FeaturesSection page={page} />
      <ServicesSection page={page} />
      <PricingSection page={page} />
      <TestimonialsSection page={page} />
      <OpeningHoursSection page={page} />
      <GallerySection page={page} />
      <CallToActionSection page={page} />
      <ContactSection page={page} />
      <SocialLinksSection page={page} />
      <LocationSection page={page} />
      <FaqSection page={page} />
      <SeoSection page={page} />
    </main>
  );
}
