import { stringify } from 'qs';
import request from '../utils/request';

const ROOT_PATH = '/report';

export async function fetch(params) {
  const options = {
    method: 'GET',
    body: {
      ...params,
    },
  };
  return request(`${ROOT_PATH}/list`, options);
}
