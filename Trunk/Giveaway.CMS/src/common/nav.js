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
        name: 'Quản lý bài đăng',
        path: 'post-management',
        icon: 'news',
        children: [
          {
            name: 'List post',
            path: '',
            component: dynamicWrapper(app, ['postManagement'], () => import('../routes/PostManagement/PostList')),
          },
          {
            name: 'Detail',
            path: 'detail/:id',
            component: dynamicWrapper(app, ['postManagement'], () => import('../routes/PostManagement/Detail')),
          },
        ],
      },
      {
        name: 'Quản lý người dùng',
        path: 'user-management',
        icon: 'user',
        children: [
          {
            name: 'List User',
            path: '',
            component: dynamicWrapper(app, ['userManagement'], () => import('../routes/UserManagement/UserList')),
          },
          {
            name: 'Detail',
            path: 'detail/:id',
            component: dynamicWrapper(app, ['userManagement'], () => import('../routes/UserManagement/Detail')),
          },
        ],
      },
      {
        name: 'Quản lý danh mục',
        path: 'category-management',
        icon: 'gift',
        children: [
          {
            name: 'List category',
            path: '',
            component: dynamicWrapper(app, ['categoryManagement'], () => import('../routes/CategoryManagement/CategoryList')),
          },
          {
            name: 'Detail',
            path: 'detail/:id',
            component: dynamicWrapper(app, ['categoryManagement'], () => import('../routes/CategoryManagement/Detail')),
          },
          {
            name: 'Create new category',
            path: 'create',
            component: dynamicWrapper(app, ['categoryManagement'], () => import('../routes/CategoryManagement/Create')),
          },
        ],
      },
      {
        name: 'Quản lý báo cáo',
        path: 'report-management',
        icon: 'sms',
        children: [
          {
            name: 'List report',
            path: '',
            component: dynamicWrapper(app, ['reportManagement', 'userManagement'], () => import('../routes/ReportManagement/ReportList')),
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

