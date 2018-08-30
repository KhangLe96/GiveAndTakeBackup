import React from 'react';
import { connect } from 'dva';
import { Table, Divider, Card, Button, Spin, Popconfirm } from 'antd';
import { Link, routerRedux } from 'dva/router';
import moment from 'moment';
import styles from './index.less';
import { COLOR, DateFormatDisplay, ENG_VN_DICTIONARY, STATUSES, TABLE_PAGESIZE } from '../../../common/constants';

@connect(({ modals, categoryManagement }) => ({
  ...modals, categoryManagement,
}))

export default class index extends React.Component {
  constructor(props) {
    super(props);
    this.onPageNumberChange = this.onPageNumberChange.bind(this);
  }

  componentWillMount() {
    const { dispatch } = this.props;
    const pageSize = TABLE_PAGESIZE;
    const page = this.props.categoryManagement.currentPage;
    const payload = { page, limit: pageSize };
    dispatch({
      type: 'categoryManagement/getCategories',
      payload,
    });
  }

  onPageNumberChange(page, pageSize) {
    const { dispatch } = this.props;
    const payload = { page, limit: pageSize };
    dispatch({
      type: 'categoryManagement/getCategories',
      payload,
    });
  }

  columns =
    [
      {
        title: 'Tên danh mục',
        key: 'categoryName',
        render: record => <Link to={`/category-management/detail/${record.id}`}>{record.categoryName}</Link>,
      },
      {
        title: 'Ngày đăng',
        dataIndex: 'createdTime',
        key: 'createdTime',
        render: val => <span>{moment.utc(val).local().format(DateFormatDisplay)}</span>,
      },
      {
        title: 'Trạng thái',
        dataIndex: 'status',
        key: 'CMSStatus',
        render: val => ENG_VN_DICTIONARY[val],
      },
      {
        title: 'Hành động',
        key: 'Action',
        render: (record) => {
          let buttonContent = 'Khóa';
          let buttonIcon = 'lock';
          let newCMSStatus = STATUSES.Blocked;
          let popConfirmTitle = 'Bạn chắc chắn muốn khóa danh mục này?';
          if (record.status === STATUSES.Blocked) {
            buttonContent = 'Mở khóa';
            buttonIcon = 'unlock';
            newCMSStatus = STATUSES.Activated;
            popConfirmTitle = 'Bạn có muốn mở lại danh mục này?';
          }
          return (
            <span>
              <Popconfirm
                title={popConfirmTitle}
                onConfirm={() => {
                  const { categoryManagement: { currentPage }, dispatch } = this.props;
                  dispatch({
                    type: 'categoryManagement/changeCMSStatus',
                    payload: { CMSStatus: newCMSStatus, id: record.id, page: currentPage },
                  });
                }}
              >
                <Button type="primary" icon={buttonIcon} className={styles.buttonStyle}>{buttonContent}</Button>
              </Popconfirm>
              <Divider type="vertical" />
              <Popconfirm
                title="Bạn muốn xóa danh mục này?"
                onConfirm={() => {
                  const { categoryManagement: { currentPage }, dispatch } = this.props;
                  dispatch({
                    type: 'categoryManagement/delete',
                    payload: { id: record.id, page: currentPage },
                  });
                }}
              >
                <Button type="danger" icon="delete" disabled={record.doesHaveAnyPost} className={styles.buttonStyle}>Xóa</Button>
              </Popconfirm>
            </span>
          );
        },
      },
    ];

  render() {
    const { categoryManagement: { categories, currentPage, totals } } = this.props;
    return (
      <div>
        <div className="containerHeader">
          <h1>Quản lý danh mục</h1>
          <Link to="/category-management/create" >
            <Button className="rightButton" type="primary">Tạo mới</Button>
          </Link>
        </div>
        <div className="containerBody">
          <Table
            columns={this.columns}
            dataSource={categories && categories.map((post, key) => { return { ...post, key }; })}
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
