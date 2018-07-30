import dynamic from 'dva/dynamic';

// wrapper of dynamic
const dynamicWrapper = (app, models, component) => dynamic({
  app,
  models: () => models.map(m => import(`../models/${m}.js`)),
  component,
});

export const getNavData = app => [
  {
    component: dynamicWrapper(app, ['passport'], () => import('../layouts/BasicLayout')),
    layout: 'BasicLayout',
    name: 'Trang chủ',
    path: '',
    children: [
      {
        name: 'Thống kê',
        path: 'dashboard',
        icon: 'dashboard',
        component: dynamicWrapper(app, ['dashboard'], () => import('../routes/Dashboard')),
      },
      {
        name: 'Report Management',
        path: 'reportmanagement',
        icon: 'data',
        children: [
          {
            name: 'report',
            path: '',
            component: dynamicWrapper(app, ['reportmanagement'], () => import('../routes/Reportmanagement')),
          },
          {
            name: 'Detail',
            path: 'report/:id',
            component: dynamicWrapper(app, ['passport'], () => import('../routes/Auth/ForgotPassword')),
          },
        ],
      },
    ],
  },
  {
    path: '/auth',
    layout: 'BlankLayout',
    component: dynamicWrapper(app, ['modals'], () => import('../layouts/BlankLayout')),
    children: [
      {
        name: 'auth',
        path: 'auth',
        visible: false,
        children: [
          {
            name: 'Login',
            path: 'login',
            component: dynamicWrapper(app, ['passport'], () => import('../routes/Auth/Login')),
          },
          {
            name: 'ForgotPassword',
            path: 'forgot-password',
            component: dynamicWrapper(app, ['passport'], () => import('../routes/Auth/ForgotPassword')),
          },
          {
            name: 'VerificationCode',
            path: 'verification-code',
            component: dynamicWrapper(app, ['passport'], () => import('../routes/Auth/VerificationCode')),
          },
          {
            name: 'NewPassword',
            path: 'new-password',
            component: dynamicWrapper(app, ['passport'], () => import('../routes/Auth/NewPassword')),
          },
        ],
      },
    ],
  },
];

