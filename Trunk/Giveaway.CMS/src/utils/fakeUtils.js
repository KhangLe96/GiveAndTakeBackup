import data from '../routes/Auth/FakeData/users.json';

export function fakelogin(username, password) {
  const { users } = data;
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
