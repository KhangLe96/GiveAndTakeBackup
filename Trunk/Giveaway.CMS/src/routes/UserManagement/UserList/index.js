import React from 'react';
import { Table, Icon, Divider, Card, Button, Spin, Popconfirm, Input } from 'antd';
import { Link } from 'dva/router';

export default class index1 extends React.Component {
  columns =
    [
      {
        title: 'ID',
        dataIndex: 'userId',
        key: 'userId',
        value: 'abc',
        render: val => <Link to={`/post-management/detail/${val}`}>{val}</Link>,
      },
      {
        title: 'Name',
        dataIndex: 'userName',
        key: 'userName',
      },
      {
        title: 'Address',
        dataIndex: 'userAddress',
        key: 'userAddress',
      },
      {
        title: 'Email',
        dataIndex: 'email',
        key: 'email',

      }, {
        title: 'Role',
        dataIndex: 'role',
        key: 'role',
      }, {
        title: 'Action',
        dataIndex: 'userId',
        key: 'Action',
        render: id => (
          <span>
            {/* <Popconfirm
              title="Bạn chắc chắn muốn xóa?"
              // onConfirm={() => {
              //   const { dispatch, users } = this.props;
              //   dispatch({
              //     type: 'postManagement/deletePost',
              //     payload: { posts, id },
              //   });
              }}
            >
              <button>
                <Icon type="delete" />
              </button>
            </Popconfirm> */}
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <Popconfirm
              title="Ban User?"
            >
              <button>
                <Icon type="warning" />
              </button>
            </Popconfirm>
          </span >),
      },
    ];

  render() {
    const { users } = this.props;
    return (
      <Table
        columns={this.columns}
        dataSource={users.map((user, key) => { return { ...user, key }; })}
        pagination={{ pageSize: 10 }}
      />
    );
  }
}
