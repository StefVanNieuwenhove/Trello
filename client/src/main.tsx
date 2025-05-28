import { StrictMode } from 'react';
import { createRoot } from 'react-dom/client';
import './index.css';
import App from './App.tsx';
import { BrowserRouter, Route, Routes } from 'react-router';
import NotFoundPage from './pages/NotFoundPage.tsx';
import { AuthLayout, ScreenSize } from './components/index.ts';
import { SignInPage, SignUpPage } from './pages/index.ts';

createRoot(document.getElementById('root')!).render(
  <StrictMode>
    <BrowserRouter>
      <Routes>
        <Route index element={<App />} />
        <Route element={<AuthLayout />}>
          <Route path='sign-in' element={<SignInPage />} />
          <Route path='sign-up' element={<SignUpPage />} />
        </Route>
        <Route path='*' element={<NotFoundPage />} />
      </Routes>
    </BrowserRouter>
    <ScreenSize />
  </StrictMode>
);
