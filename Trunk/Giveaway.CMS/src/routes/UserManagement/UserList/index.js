import React from 'react';
import { Table, Icon, Divider, Card, Button, Spin, Popconfirm, Input } from 'antd';
import { Link, routerRedux } from 'dva/router';
import { TABLE_PAGESIZE } from '../../../common/constants';

export default class index extends React.Component {
  constructor(props) {
    super(props);
    this.onPageNumberChange = this.onPageNumberChange.bind(this);
  }

  onPageNumberChange(page, pageSize) {
    const { dispatch } = this.props;
    const payload = { page, limit: pageSize };
    dispatch({
      type: 'userManagement/fetch',
      payload,
    });
  }

  columns =
    [
      {
        title: 'Tên',
        // dataIndex: 'firstName',
        key: 'firstName',
        render: record => <Link to={`/user-management/detail/${record.id}`}>{record.username}</Link>,
      },
      {
        title: 'Địa chỉ',
        dataIndex: 'address',
        key: 'address',
      },
      {
        title: 'Trạng thái',
        dataIndex: 'status',
        key: 'status',

      }, {
        title: 'Role',
        dataIndex: 'role',
        key: 'role',
      }, {
        title: 'Hành động',
        key: 'Action',
        render: record => (
          <span>
            <Popconfirm
              title="Bạn chắc chắn không ?"
              onConfirm={() => {
                const { users, totals, dispatch } = this.props;
                const newStatus = record.status === 'Blocked' ? 'Activated' : 'Blocked';
                dispatch({
                  type: 'userManagement/changeStatus',
                  payload: { newStatus, id: record.id },
                })
              }
              }
            >
              <Icon type="warning" />
            </Popconfirm>
          </span >),
      },
    ];
  // /Review: Should use button with both text and icon to clear. Status converts to Vietnamese pls.
  // /Role should have comma between roles likes User, Admin. Add one button for block use here.
  // /If data is null, we should display N/A
  render() {
    const { users, currentPage, totals } = this.props;
    return (
      <Table
        columns={this.columns}
        dataSource={users.map((user, key) => { return { ...user, key }; })}
        pagination={{
          current: currentPage,
          onChange: this.onPageNumberChange,
          pageSize: TABLE_PAGESIZE,
          total: totals,
        }}
      />
    );
  }
}
