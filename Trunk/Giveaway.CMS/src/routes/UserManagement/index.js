import React from 'react';
import { connect } from 'dva';
import UserList from './UserList';
import { Input, Select, Button } from 'antd';

@connect(({ modals, userManagement }) => ({
  ...modals, ...userManagement,
}))
export default class index extends React.Component {
  componentDidMount() {
    const { users, dispatch } = this.props;
    // Review: remove check length here, allow fetch new data
    if (users.length === 0) {
      dispatch({
        type: 'userManagement/fetch',
        payload: {},
      });
    }
  }


  render() {
    const { users, currentPage, dispatch, totals } = this.props;
    return (
      <div>
        <div className="containerHeader">
          <h1>Quản lý người dùng</h1>
        </div>
        <div className="containerBody">
          <UserList
            users={users}
            currentPage={currentPage}
            dispatch={dispatch}
            totals={totals}
          />
        </div>
      </div>
    );
  }
}
index.defaultProps = { users: [], totals: 0 };
