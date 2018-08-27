import * as uuid from 'uuid';
import Base64    from './base64';

const dummyAvatar = () => {
  return `https://api.adorable.io/avatars/285/${uuid.v1()}.png`;
};

const dummyToken = () => {
  return Base64.encode(uuid.v1());
};

export default {
  dummyAvatar,
  dummyToken,
};
