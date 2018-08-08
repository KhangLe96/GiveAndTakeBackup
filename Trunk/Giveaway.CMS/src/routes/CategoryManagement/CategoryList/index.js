import React from 'react';
import { Table, Icon, Divider, Card, Button, Spin, Popconfirm } from 'antd';
import { Link, routerRedux } from 'dva/router';
import moment from 'moment';

import { DateFormatDisplay, TABLE_PAGESIZE } from '../../../common/constants';

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

  // /Review: should have status column, if it is necessary, don't hesitate to request API team to change sth
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
        title: 'Hành động',
        key: 'Action',
        render: record => (
          <span>
            <Popconfirm
              title="Bạn chắc chắn muốn xóa?"
              onConfirm={() => {
                const { dispatch, totals } = this.props;
                dispatch({
                  type: 'categoryManagement/delete',
                  payload: { id: record.id, totals },
                });
              }}
            >
              <Icon type="delete" />
            </Popconfirm>
          </span>),
      },
    ];
  // /Review: Should use button with text and icon to clear. Should have edit button, change status.

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
