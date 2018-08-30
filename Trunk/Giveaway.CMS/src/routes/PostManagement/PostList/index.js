import React from 'react';
import { Table, Divider, Button, Popconfirm } from 'antd';
import { connect } from 'dva';
import { Link } from 'dva/router';
import moment from 'moment';
import { DateFormatDisplay, TABLE_PAGESIZE, ENG_VN_DICTIONARY, COLOR, STATUSES } from '../../../common/constants';
import styles from './index.less';
@connect(({ modals, postManagement }) => ({
  ...modals, postManagement,
}))

export default class index extends React.Component {
  constructor(props) {
    super(props);
    this.onPageNumberChange = this.onPageNumberChange.bind(this);
  }

  componentWillMount() {
    const { dispatch } = this.props;
    const pageSize = TABLE_PAGESIZE;
    const page = this.props.postManagement.currentPage;
    const payload = { page, limit: pageSize };
    dispatch({
      type: 'postManagement/fetch',
      payload,
    });
  }

  onPageNumberChange(page, pageSize) {
    const { dispatch } = this.props;
    const payload = { page, limit: pageSize };
    dispatch({
      type: 'postManagement/fetch',
      payload,
    });
  }

  handleConfirmChangeStatus = (record) => {
    const { dispatch, postManagement: { currentPage } } = this.props;
    const statusCMS = record.statusCMS === STATUSES.Blocked ? STATUSES.Activated : STATUSES.Blocked;
    dispatch({
      type: 'postManagement/changePostCMSStatus',
      payload: { statusCMS, id: record.id, page: currentPage },
    });
  }

  handleDisplayStatusButton = (record) => {
    return (record.status === STATUSES.Activated ? STATUSES.Blocked : STATUSES.Activated);
  }
  columns =
    [
      {
        title: 'Tiêu đề',
        key: 'title',
        render: record => <Link to={`/post-management/detail/${record.id}`}>{record.title}</Link>,
      },
      {
        title: 'Người đăng',
        key: 'user',
        render: record => <Link to={`/user-management/detail/${record.user.id}`}>{record.user.username}</Link>,
      },
      {
        title: 'Địa chỉ',
        dataIndex: 'address.provinceCityName',
        key: 'address.provinceCityName',
      },
      {
        title: 'Danh mục',
        dataIndex: 'category',
        key: 'category.categoryName',
        render: category => <Link to={`/category-management/detail/${category.id}`}>{category.categoryName}</Link>,

      },
      {
        title: 'Ngày đăng',
        dataIndex: 'createdTime',
        key: 'dayPost',
        render: val => <span>{moment.utc(val).local().format(DateFormatDisplay)}</span>,
      },
      {
        title: 'Trạng thái',
        dataIndex: 'statusCMS',
        key: 'statusCMS',
        render: val => ENG_VN_DICTIONARY[val],
      }, {
        title: 'Hành động',
        key: 'Action',
        render: (record) => {
          let buttonContent = 'Khóa';
          let buttonIcon = 'lock';
          let newPostStatus = STATUSES.Blocked;
          let popConfirmTitle = 'Bạn chắc chắn muốn khóa bài đăng này?';
          if (record.statusCMS === STATUSES.Blocked) {
            buttonContent = 'Mở khóa';
            buttonIcon = 'unlock';
            newPostStatus = STATUSES.Activated;
            popConfirmTitle = 'Bạn có muốn mở lại bài đăng này?';
          }
          return (
            <span>
              <Popconfirm
                title={popConfirmTitle}
                onConfirm={() => this.handleConfirmChangeStatus(record)}
              >
                <Button type="primary" icon={buttonIcon} className={styles.buttonStyle}>{buttonContent}</Button>
              </Popconfirm>
            </span >
          );
        },
      },
    ];

  render() {
    const { postManagement: { posts, currentPage, totals } } = this.props;
    return (
      <div>
        <div className="containerHeader">
          <h1>Quản lý bài đăng</h1>
        </div>
        <div className="containerBody">
          <Table
            columns={this.columns}
            dataSource={posts && posts.map((post, key) => { return { ...post, key }; })}
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
