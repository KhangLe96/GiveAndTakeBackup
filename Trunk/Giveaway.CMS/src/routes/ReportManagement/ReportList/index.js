import React from 'react';
import { Table, Divider, Button, Popconfirm } from 'antd';
import { connect } from 'dva';
import { Link } from 'dva/router';
import moment from 'moment';
import { DateFormatDisplay, TABLE_PAGESIZE, ENG_VN_DICTIONARY, COLOR, STATUSES } from '../../../common/constants';
import styles from './index.less';
@connect(({ modals, reportManagement, userManagement }) => ({
  ...modals, reportManagement, userManagement,
}))

export default class index extends React.Component {
  constructor(props) {
    super(props);
    this.onPageNumberChange = this.onPageNumberChange.bind(this);
  }

  componentWillMount() {
    const { dispatch } = this.props;
    const pageSize = TABLE_PAGESIZE;
    const page = this.props.reportManagement.currentPage;
    const payload = { page, limit: pageSize };
    dispatch({
      type: 'reportManagement/fetch',
      payload,
    });
  }

  onPageNumberChange(page, pageSize) {
    const { dispatch } = this.props;
    const payload = { page, limit: pageSize };
    dispatch({
      type: 'reportManagement/fetch',
      payload,
    });
  }

  // handleConfirmWarning = (record) => {
  //   const { dispatch, reportManagement: { currentPage } } = this.props;
  //   const statusCMS = record.statusCMS === STATUSES.Blocked ? STATUSES.Activated : STATUSES.Blocked;
  //   dispatch({
  //     type: 'postManagement/changePostCMSStatus',
  //     payload: { statusCMS, id: record.id, page: currentPage },
  //   });
  // }

  handleConfirmChangeStatus = (record) => {
    const { dispatch, reportManagement: { currentPage } } = this.props;
    const newStatus = record.status === STATUSES.Blocked ? STATUSES.Activated : STATUSES.Blocked;
    dispatch({
      type: 'userManagement/changeStatus',
      payload: { newStatus, id: record.id, page: currentPage },
      callback: () => this.onPageNumberChange(currentPage, TABLE_PAGESIZE),
    });
  }

  columns =
    [
      {
        title: 'Tiêu đề bài đăng',
        key: 'title',
        render: record => <Link to={`/post-management/detail/${record.post.id}`}>{record.post.title}</Link>,
      },
      {
        title: 'Người đăng',
        key: 'user',
        render: record => <Link to={`/user-management/detail/${record.user.id}`}>{record.user.username}</Link>,
      },
      {
        title: 'Ngày báo cáo',
        dataIndex: 'createdTime',
        key: 'createdTime',
        render: val => <span>{moment.utc(val).local().format(DateFormatDisplay)}</span>,
      },
      {
        title: 'Nội dung báo cáo',
        dataIndex: 'message',
        key: 'message',
      },
      {
        title: 'Số lần người dùng bị cảnh báo',
        dataIndex: 'warningNumber',
        key: 'report.warningNumber',
      },
      {
        title: 'Hành động',
        render: (record) => {
          let buttonContent = 'Khóa';
          let buttonIcon = 'lock';
          let newPostStatus = STATUSES.Blocked;
          let popConfirmTitle = 'Bạn chắc chắn muốn khóa người dùng này?';
          if (record.user.status === STATUSES.Blocked) {
            buttonContent = 'Mở khóa';
            buttonIcon = 'unlock';
            newPostStatus = STATUSES.Activated;
            popConfirmTitle = 'Bạn có muốn mở lại người dùng này?';
          }

          return (
            <span>
              <Popconfirm
                title="Bạn chắc chắn muốn cảnh báo người dùng này"
                onConfirm={() => this.handleConfirmWarning(record)}
              >
                <Button type="primary" icon={buttonIcon} className={styles.buttonStyle}>Cảnh báo</Button>
              </Popconfirm>
              <Popconfirm
                title={popConfirmTitle}
                onConfirm={() => this.handleConfirmChangeStatus(record.user)}
              >
                <Button type="primary" icon={buttonIcon} className={styles.buttonStyle}>{buttonContent}</Button>
              </Popconfirm>
            </span >
          );
        },
      },
    ];

  render() {
    const { reportManagement: { reports, currentPage, totals } } = this.props;
    return (
      <div>
        <div className="containerHeader">
          <h1>Quản lý báo cáo</h1>
        </div>
        <div className="containerBody">
          <Table
            columns={this.columns}
            dataSource={reports && reports.map((report, key) => { return { ...report, key }; })}
            pagination={{
              current: currentPage,
              onChange: this.onPageNumberChange,
              pageSize: TABLE_PAGESIZE,
              total: totals,
            }}
            dispatch={this.props.dispatch}
          />
        </div>
      </div>
    );
  }
}