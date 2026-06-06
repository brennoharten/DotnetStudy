export function Section({
  id,
  title,
  children
}: {
  id?: string;
  title: string;
  children: React.ReactNode;
}) {
  return (
    <section id={id} className="mx-auto w-full max-w-6xl px-6 py-14">
      <h2 className="mb-6 text-2xl font-semibold text-white">{title}</h2>
      {children}
    </section>
  );
}
