// fake data
import userData from '../fakeData/user.json';
import postData from '../fakeData/post.json';

export function fakelogin(username, password) {
  const { users } = userData;
  for (let index = 0; index < users.length; index += 1) {
    const user = users[index];
    if (user.username === username && user.password === password) {
      return (
        {
          access_token: 'asdkjxklcj12asdlsd',
        }
      );
    }
  }
  return false;
}

export function fakeFetchPost() {
  return postData.posts;
}

export function fakeDeletePost() {
  return true;
}

export function fakeFetchUser() {
  return userData.users;
}
