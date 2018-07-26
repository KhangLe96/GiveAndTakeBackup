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
        name: 'Post Management',
        path: 'postmanagement',
        icon: 'data',
        component: dynamicWrapper(app, ['dashboard'], () => import('../routes/Dashboard/postmanagement')),
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

