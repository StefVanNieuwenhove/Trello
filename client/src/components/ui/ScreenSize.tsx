import { Badge } from './badge';

const ScreenSize = () => {
  if (process.env.NODE_ENV !== 'development') return null;

  const screenSizes = [
    {
      label: 'XS',
      classNames: 'block sm:hidden md:hidden lg:hidden xl:hidden bg-gray-700',
    },
    {
      label: 'SM',
      classNames: 'block md:hidden lg:hidden xl:hidden bg-violet-700',
    },
    {
      label: 'MD',
      classNames: 'hidden sm:hidden md:block lg:hidden xl:hidden bg-sky-700',
    },
    {
      label: 'LG',
      classNames: 'hidden sm:hidden md:hidden lg:block xl:hidden bg-lime-700',
    },
    {
      label: 'XL',
      classNames: 'hidden sm:hidden md:hidden lg:hidden xl:block bg-red-700',
    },
  ];

  return (
    <>
      {screenSizes.map((size) => (
        <Badge
          key={size.label}
          className={`fixed bottom-0 right-0 z-50 p-3 m-2 rounded-4xl text-white text-sm ${size.classNames}`}>
          {size.label}
        </Badge>
      ))}
    </>
  );
};

export default ScreenSize;
