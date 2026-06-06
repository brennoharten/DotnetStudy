import { Section } from './Section';
import type { PublicLandingPage } from '@/types/landing-page';

const cardClass = 'rounded-2xl border border-white/10 bg-white/5 p-6 shadow-lg shadow-black/10';

export function HeroSection({ page }: { page: PublicLandingPage }) {
  return (
    <header className="mx-auto grid w-full max-w-6xl gap-8 px-6 py-16 lg:grid-cols-2 lg:items-center">
      <div className="space-y-6">
        <p className="text-sm uppercase tracking-[0.3em] text-cyan-300">{page.tags.join(' • ')}</p>
        <h1 className="text-4xl font-bold leading-tight text-white md:text-6xl">{page.title}</h1>
        <p className="max-w-2xl text-lg text-slate-300">{page.subtitle}</p>
        <p className="max-w-2xl text-slate-300">{page.description}</p>
        {page.callToAction.text && (
          <a
            href={page.callToAction.buttonUrl || '#contact'}
            className="inline-flex items-center rounded-full bg-cyan-400 px-6 py-3 font-semibold text-slate-950 transition hover:bg-cyan-300"
          >
            {page.callToAction.buttonText || page.callToAction.text}
          </a>
        )}
      </div>
      {page.heroImageUrl && (
        <div className="overflow-hidden rounded-3xl border border-white/10 bg-white/5">
          {/* eslint-disable-next-line @next/next/no-img-element */}
          <img src={page.heroImageUrl} alt={page.title} className="h-full w-full object-cover" />
        </div>
      )}
    </header>
  );
}

export function FeaturesSection({ page }: { page: PublicLandingPage }) {
  if (!page.features?.length) return null;
  return (
    <Section title="Features">
      <div className="grid gap-4 sm:grid-cols-2 lg:grid-cols-3">
        {page.features.map((feature) => (
          <div key={feature} className={cardClass}>
            {feature}
          </div>
        ))}
      </div>
    </Section>
  );
}

export function ServicesSection({ page }: { page: PublicLandingPage }) {
  if (!page.services?.length) return null;
  return (
    <Section title="Serviços">
      <div className="grid gap-4 md:grid-cols-2 xl:grid-cols-3">
        {page.services.map((service) => (
          <article key={service.name} className={cardClass}>
            <h3 className="mb-2 text-xl font-semibold">{service.name}</h3>
            <p className="text-slate-300">{service.description}</p>
          </article>
        ))}
      </div>
    </Section>
  );
}

export function PricingSection({ page }: { page: PublicLandingPage }) {
  if (!page.pricingPlans?.length) return null;
  return (
    <Section title="Planos">
      <div className="grid gap-4 md:grid-cols-2">
        {page.pricingPlans.map((plan) => (
          <article key={plan.name} className={cardClass}>
            <h3 className="text-xl font-semibold">{plan.name}</h3>
            <p className="mt-2 text-3xl font-bold text-cyan-300">
              R$ {plan.price.toFixed(2)} <span className="text-base text-slate-400">/ {plan.interval}</span>
            </p>
            <ul className="mt-4 list-disc space-y-1 pl-5 text-slate-300">
              {plan.benefits.map((benefit) => (
                <li key={benefit}>{benefit}</li>
              ))}
            </ul>
          </article>
        ))}
      </div>
    </Section>
  );
}

export function ContactSection({ page }: { page: PublicLandingPage }) {
  if (!page.contact?.email && !page.contact?.phone && !page.contact?.address) return null;
  return (
    <Section id="contact" title="Contato">
      <div className={cardClass}>
        <p>{page.contact.phone}</p>
        <p>{page.contact.email}</p>
        <p>{page.contact.address}</p>
      </div>
    </Section>
  );
}

export function SocialLinksSection({ page }: { page: PublicLandingPage }) {
  if (!page.socialLinks?.length) return null;
  return (
    <Section title="Redes sociais">
      <div className="flex flex-wrap gap-3">
        {page.socialLinks.map((link) => (
          <a key={link.platform} href={link.url} className="rounded-full border border-white/10 px-4 py-2 text-slate-200">
            {link.platform}
          </a>
        ))}
      </div>
    </Section>
  );
}

export function TestimonialsSection({ page }: { page: PublicLandingPage }) {
  if (!page.testimonials?.length) return null;
  return (
    <Section title="Depoimentos">
      <div className="grid gap-4 md:grid-cols-2">
        {page.testimonials.map((testimonial) => (
          <blockquote key={testimonial.author} className={cardClass}>
            <p className="text-slate-200">“{testimonial.content}”</p>
            <footer className="mt-4 text-sm text-slate-400">
              {testimonial.author} — {testimonial.role}
            </footer>
          </blockquote>
        ))}
      </div>
    </Section>
  );
}

export function OpeningHoursSection({ page }: { page: PublicLandingPage }) {
  const entries = Object.entries(page.openingHours ?? {});
  if (!entries.length) return null;
  return (
    <Section title="Horários de funcionamento">
      <div className={cardClass}>
        <dl className="grid gap-2 sm:grid-cols-2">
          {entries.map(([day, hours]) => (
            <div key={day} className="flex justify-between gap-4 border-b border-white/10 py-2 last:border-b-0">
              <dt className="font-medium">{day}</dt>
              <dd className="text-slate-300">{hours}</dd>
            </div>
          ))}
        </dl>
      </div>
    </Section>
  );
}

export function GallerySection({ page }: { page: PublicLandingPage }) {
  if (!page.galleryUrls?.length) return null;
  return (
    <Section title="Galeria">
      <div className="grid gap-4 sm:grid-cols-2 lg:grid-cols-3">
        {page.galleryUrls.map((image) => (
          // eslint-disable-next-line @next/next/no-img-element
          <img key={image} src={image} alt={page.title} className="h-56 w-full rounded-2xl object-cover" />
        ))}
      </div>
    </Section>
  );
}

export function CallToActionSection({ page }: { page: PublicLandingPage }) {
  if (!page.callToAction?.text) return null;
  return (
    <Section title="Próximo passo">
      <div className="rounded-3xl bg-cyan-400 p-8 text-slate-950">
        <p className="text-2xl font-semibold">{page.callToAction.text}</p>
        <a href={page.callToAction.buttonUrl || '#contact'} className="mt-4 inline-flex rounded-full bg-slate-950 px-5 py-3 font-semibold text-white">
          {page.callToAction.buttonText || 'Saiba mais'}
        </a>
      </div>
    </Section>
  );
}

export function SeoSection({ page }: { page: PublicLandingPage }) {
  if (!page.seo?.description && !page.seo?.keywords?.length) return null;
  return (
    <Section title="SEO">
      <div className={cardClass}>
        <p className="font-semibold">{page.seo.title}</p>
        <p className="mt-2 text-slate-300">{page.seo.description}</p>
        {page.seo.keywords?.length ? <p className="mt-4 text-sm text-slate-400">Palavras-chave: {page.seo.keywords.join(', ')}</p> : null}
      </div>
    </Section>
  );
}

export function LocationSection({ page }: { page: PublicLandingPage }) {
  if (!page.location?.address) return null;
  return (
    <Section title="Localização">
      <div className="grid gap-4 lg:grid-cols-2">
        <div className={cardClass}>
          <p>{page.location.address}</p>
          {typeof page.location.latitude === 'number' && typeof page.location.longitude === 'number' ? (
            <p className="mt-2 text-slate-400">
              {page.location.latitude}, {page.location.longitude}
            </p>
          ) : null}
        </div>
        {page.location.googleMapsEmbed ? (
          <div className="overflow-hidden rounded-2xl border border-white/10" dangerouslySetInnerHTML={{ __html: page.location.googleMapsEmbed }} />
        ) : null}
      </div>
    </Section>
  );
}

export function FaqSection({ page }: { page: PublicLandingPage }) {
  if (!page.faqs?.length) return null;
  return (
    <Section title="FAQ">
      <div className="space-y-3">
        {page.faqs.map((faq) => (
          <details key={faq.question} className={cardClass}>
            <summary className="cursor-pointer font-semibold">{faq.question}</summary>
            <p className="mt-3 text-slate-300">{faq.answer}</p>
          </details>
        ))}
      </div>
    </Section>
  );
}
