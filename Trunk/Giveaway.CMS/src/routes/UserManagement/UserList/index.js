import React from 'react';
import { connect } from 'dva';
import { Table, Button, Popconfirm } from 'antd';
import { Link } from 'dva/router';
import { TABLE_PAGESIZE, STATUS_ACTIVATED, STATUS_BLOCKED, STATUS_ACTION_ACTIVATE, STATUS_ACTION_BLOCKED } from '../../../common/constants';
import { ENG_VN_DICTIONARY } from "../../../common/constants";

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
    const { dispatch, userManagement: { currentPage } } = this.props;
    const newStatus = record.status === STATUS_BLOCKED ? STATUS_ACTIVATED : STATUS_BLOCKED;
    dispatch({
      type: 'userManagement/changeStatus',
      payload: { newStatus, id: record.id, page: currentPage },
    });
  }

  handleDisplayStatusButton = (record) => {
    return (record.status === STATUS_ACTIVATED ? STATUS_ACTION_BLOCKED : STATUS_ACTION_ACTIVATE);
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
        render: record => (ENG_VN_DICTIONARY(record)),
      }, {
        title: 'Vai trò',
        // dataIndex: 'role',
        key: 'role',
        render: record => (ENG_VN_DICTIONARY(record)),
      }, {
        title: 'Hành động',
        key: 'Action',
        render: record => (
          <span>
            <Popconfirm
              title="Bạn chắc chắn không ?"
              onConfirm={() => this.handleConfirmChangeStatus(record)}
            >
              <Button icon="exclamation-circle-o" type="primary" style={{ width: 120 }}>
                {ENG_VN_DICTIONARY(record)}
              </Button>
            </Popconfirm>
          </span >),
      },
    ];
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
