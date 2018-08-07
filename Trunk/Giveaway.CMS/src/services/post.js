import request from '../utils/request';
import { stringify } from 'qs';

const ROOT_PATH = '/post';

export async function fetch(params) {
  const options = {
    method: 'GET',
    body: {
      ...params,
    },
  };
  return request(`${ROOT_PATH}/getList`, options);
}

// export function delete(params) {
//   const options = {
//     method: 'DELETE',
//     body: {
//       ...params,
//     },
//   };
//   return request(`${ROOT_PATH}/delete`, options);
// }

export async function findPost(params) {
  console.log(`${ROOT_PATH}?${stringify(params)}`);
  return request(`${ROOT_PATH}?${stringify(params)}`, {
    method: 'GET',
  });
}
