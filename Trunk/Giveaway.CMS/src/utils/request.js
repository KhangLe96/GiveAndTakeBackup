import fetch from 'dva/fetch';
import conf from 'json!../common/conf.json';
import { stringify } from 'qs';
import { routerRedux } from 'dva/router';
import store from '../index';

function checkStatus(response) {
  if (response.status >= 200 && response.status < 300) {
    return response;
  }

  if (response.status === 401) {
    localStorage.clear();
    return;
  }

  const error = new Error(response.statusText);
  error.response = response;
  throw error;
}

function prepare_api_url(url) {
  return conf.environments[conf.active_environment].API_URL + url;
}

function prepare_api_url_upload(url) {
  return conf.environments[conf.active_environment].API_UPLOAD_URL + url;
}

function prepare_options(options) {
  const currentUser = JSON.parse(localStorage.getItem('currentUser'));
  if (currentUser && currentUser.token !== undefined) {
    options.headers = {
      ...options.headers,
      Authorization: `Bearer ${currentUser.token}`,
    };
  }
  return options;
}

function checkEmptyResponse(response) {
  return response.text();
}

function prepareJsonData(text_response) {
  if (text_response.length === 0) {
    return {};
  }
  return JSON.parse(text_response);
}

/**
 * Requests a URL, returning a promise.
 *
 * @param  {string} url       The URL we want to request
 * @param  {object} [options] The options we want to pass to "fetch"
 * @return {object}           An object containing either "data" or "err"
 */
export default function request(url, options) {
  url = prepare_api_url(url);
  const defaultOptions = {
    credentials: 'include',
  };
  let opts = options || {};
  opts = prepare_options(opts);
  const newOptions = { ...defaultOptions, ...opts };
  if (newOptions.method === 'GET') {
    url += `/?${stringify(newOptions.body)}`;
    newOptions.body = undefined;
  }
  if (newOptions.method === 'POST'
    || newOptions.method === 'PUT'
    || newOptions.method === 'PATCH'
    || newOptions.method === 'DELETE') {
    newOptions.headers = {
      Accept: 'application/json',
      'Content-Type': 'application/json; charset=utf-8',
      ...newOptions.headers,
    };
    newOptions.body = JSON.stringify(newOptions.body);
  }
  return fetch(url, newOptions)
    .then(checkStatus)
    .then(checkEmptyResponse)
    .then(prepareJsonData)
    .catch((error) => {
      if (error && error.response) { return error.response.json(); }
    })
    .catch((error) => {
      return error;
    });
}

export function requestUpload(url, options) {
  url = prepare_api_url_upload(url);
  const defaultOptions = {
    credentials: 'include',
  };
  let opts = options || {};
  opts = prepare_options(opts);
  const newOptions = { ...defaultOptions, ...opts };
  if (newOptions.method === 'POST'
    || newOptions.method === 'PUT'
    || newOptions.method === 'PATCH'
    || newOptions.method === 'DELETE') {
    newOptions.headers = {
      Accept: 'application/json',
      'Content-Type': 'application/json; charset=utf-8',
      ...newOptions.headers,
    };
    newOptions.body = JSON.stringify(newOptions.body);
  }
  return fetch(url, newOptions)
    .then(checkStatus)
    .then(checkEmptyResponse)
    .then(prepareJsonData)
    .catch((error) => {
      if (error && error.response) { return error.response.json(); }
    })
    .catch((error) => {
      return error;
    });
}
