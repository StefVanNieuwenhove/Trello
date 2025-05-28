import { Outlet } from 'react-router';

const AuthLayout = () => {
  return (
    <main className='container w-full max-w-screen h-screen max-h-full flex items-center justify-center'>
      <Outlet />
    </main>
  );
};

export default AuthLayout;
