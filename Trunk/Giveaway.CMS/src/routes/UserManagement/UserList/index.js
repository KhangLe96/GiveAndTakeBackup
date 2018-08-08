import React from 'react';
import { connect } from 'dva';
import { Table, Icon, Divider, Card, Button, Spin, Popconfirm, Input } from 'antd';
import { Link, routerRedux } from 'dva/router';
import { TABLE_PAGESIZE } from '../../../common/constants';

@connect(({ modals, userManagement }) => ({
  ...modals, userManagement,
}))
export default class index extends React.Component {
  constructor(props) {
    super(props);
    this.onPageNumberChange = this.onPageNumberChange.bind(this);
  }

  componentWillMount() {
    const { dispatch } = this.props;
    const pageSize = TABLE_PAGESIZE;
    const page = this.props.userManagement.currentPage;
    const payload = { page, limit: pageSize };
    dispatch({
      type: 'userManagement/fetch',
      payload,
    });
  }

  onPageNumberChange(page, pageSize) {
    const { dispatch } = this.props;
    const payload = { page, limit: pageSize };
    dispatch({
      type: 'userManagement/fetch',
      payload,
    });
  }

  handleConfirmChangeStatus = (record) => {
    const { users, totals, dispatch, currentPage } = this.props;
    const newStatus = record.status === 'Blocked' ? 'Activated' : 'Blocked';
    dispatch({
      type: 'userManagement/changeStatus',
      payload: { newStatus, id: record.id, page: currentPage },
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
        // dataIndex: 'status',
        key: 'status',
        render: record => (
          record.status === 'Activated' ? 'Kích hoạt' : 'Khóa'
        ),
      }, {
        title: 'Vai trò',
        // dataIndex: 'role',
        key: 'role',
        render: record => (
          record.role[0] === 'User' ? 'Người dùng' : 'Quản lý'
        ),
      }, {
        title: 'Hành động',
        key: 'Action',
        render: record => (
          <span>
            <Popconfirm
              title="Bạn chắc chắn không ?"
              onConfirm={() => {
                const { users, totals, dispatch, userManagement: { currentPage } } = this.props;
                const newStatus = record.status === 'Blocked' ? 'Activated' : 'Blocked';
                dispatch({
                  type: 'userManagement/changeStatus',
                  payload: { newStatus, id: record.id, page: currentPage },
                });
              }
              }
            >
              <Button icon="exclamation-circle-o" type="primary" style={{ width: 120 }}>
                {record.status === 'Activated' ? 'Khóa' : 'Kích hoạt'}
              </Button>
            </Popconfirm>
          </span >),
      },
    ];
  // /Review: Should use button with both text and icon to clear. Status converts to Vietnamese pls.
  // /Role should have comma between roles likes User, Admin. Add one button for block use here.
  // /If data is null, we should display N/A
  render() {
    const { userManagement: { users, currentPage, totals } } = this.props;
    return (
      <div>
        <div className="containerHeader">
          <h1>Quản lý người dùng</h1>
        </div>
        <div className="containerBody">
          <Table
            columns={this.columns}
            dataSource={users && users.map((user, key) => { return { ...user, key }; })}
            pagination={{
              current: currentPage,
              onChange: this.onPageNumberChange,
              pageSize: TABLE_PAGESIZE,
              total: totals,
            }}
          />
        </div>
      </div>
    );
  }
}
