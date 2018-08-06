import request from '../utils/request';
import urls from '../common/urls';

// fake data
import { fakelogin } from '../utils/fakeUtils';

export async function getProfile() {
  return request(urls.getProfile);
}

export async function updateProfile(params) {
  const options = {
    method: 'PUT',
    body: {
      ...params,
    },
  };
  return request(urls.updateProfile, options);
}

export async function queryProfileMe() {
  return request('/passport/profile/me/avatar', {
    method: 'POST',
  });
}
export async function queryProvinces() {
  return request('/provinces', {
    method: 'GET',
  });
}
export async function fetchMe(params) {
  const options = {
    method: 'GET',
    body: {
      ...params,
    },
  };
  return request('/passport/profile/me', options);
}
export async function register(params) {
  const options = {
    method: 'POST',
    body: {
      ...params,
    },
  };
  return request('/passport/register', options);
}
export function login(params) {
  const options = {
    method: 'POST',
    body: {
      ...params,
    },
  };
  return request(`/user/login`, options);

  // fake data
  // return fakelogin(params.login, params.password);
}

export async function forgotPassword(params) {
  const options = {
    method: 'POST',
    body: {
      ...params,
    },
  }
  return request(urls.cmsForgotPassword, options);
}

export async function verificationCode(params) {
  const options = {
    method: 'POST',
    body: {
      ...params,
    },
  };
  return request(urls.cmsVerificationCode, options);
}

export async function newPassword(params) {
  const options = {
    method: 'POST',
    body: {
      ...params,
    },
  };
  return request(urls.cmsNewPassword, options);
}

export async function logout(params) {
  const options = {
    method: 'POST',
    body: {
      ...params,
    },
  }
  return request(urls.logout, options);
}

