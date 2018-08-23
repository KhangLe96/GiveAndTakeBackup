import React       from 'react';
import { connect } from 'dva';
import styles      from './Account.less';

const LoginLink = ({ dispatch }) => {
  const dispatchLoginModal = () => {
    dispatch(
      {
        type: 'modals/show',
        payload: { component: 'login' },
      },
    );
  };
  return (
    <div>
      <a className={styles.button_login} onClick={dispatchLoginModal}>Đăng nhập</a>
    </div>);
};

export default connect((state) => {
  return { ...state };
})(LoginLink);
