import React from 'react';
import { Table, Icon, Divider, Card, Button, Spin, Popconfirm } from 'antd';
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
      },
      {
        title: 'Hành động',
        key: 'Action',
        render: record => (
          <span>
            <Popconfirm
              title="Bạn chắc chắn muốn xóa?"
              onConfirm={() => {
                const { categories, dispatch, totals } = this.props;
                dispatch({
                  type: 'categoryManagement/delete',
                  payload: { categories, id: record.id, totals },
                });
              }}
            >
              <Icon type="delete" />
            </Popconfirm>
          </span >),
      },
    ];

  render() {
    const { categories, currentPage, totals } = this.props;
    return (
      <Table
        columns={this.columns}
        dataSource={categories.map((post, key) => { return { ...post, key }; })}
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
