import React from 'react';
import { Table, Divider, Card, Button, Spin, Popconfirm } from 'antd';
import { Link, routerRedux } from 'dva/router';
import moment from 'moment';

import { COLOR, DateFormatDisplay, ENG_VN_DICTIONARY, STATUSES, TABLE_PAGESIZE } from '../../../common/constants';

export default class index extends React.Component {
  constructor(props) {
    super(props);
    this.onPageNumberChange = this.onPageNumberChange.bind(this);
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
          let buttonType = 'danger';
          let newCMSStatus = STATUSES.Blocked;
          let popConfirmTitle = 'Bạn chắc chắn muốn khóa danh mục này?';
          if (record.status === STATUSES.Blocked) {
            buttonContent = 'Mở khóa';
            buttonIcon = 'unlock';
            buttonType = 'primary';
            newCMSStatus = STATUSES.Activated;
            popConfirmTitle = 'Bạn có muốn mở lại danh mục này?';
          }
          return (
            <span>
              <Popconfirm
                title={popConfirmTitle}
                onConfirm={() => {
                  const { currentPage, dispatch } = this.props;
                  dispatch({
                    type: 'categoryManagement/changeCMSStatus',
                    payload: { CMSStatus: newCMSStatus, id: record.id, page: currentPage },
                  });
                }}
              >
                <Button type={buttonType} icon={buttonIcon}>{buttonContent}</Button>
              </Popconfirm>
              <Divider type="vertical" />
              <Popconfirm
                title="Bạn muốn xóa danh mục này?"
                onConfirm={() => {
                  const { dispatch } = this.props;
                  dispatch({
                    type: 'categoryManagement/delete',
                    payload: { id: record.id },
                  });
                }}
              >
                <Button type="danger" icon="delete">Xóa</Button>
              </Popconfirm>
            </span>
          );
        },
      },
    ];

  render() {
    const { categories, currentPage, totals } = this.props;
    return (
      <Table
        columns={this.columns}
        dataSource={categories.map((post, key) => {
          return { ...post, key };
        })}
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
