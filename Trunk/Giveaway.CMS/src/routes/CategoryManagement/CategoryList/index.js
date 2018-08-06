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
      type: 'categoryManagement/fetch',
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
        dataIndex: 'postId',
        key: 'Action',
        render: id => (
          <span>
            <Popconfirm
              title="Bạn chắc chắn muốn xóa?"
              onConfirm={() => {
                const { dispatch, posts } = this.props;
                dispatch({
                  type: 'postManagement/deletePost',
                  payload: { posts, id },
                });
              }}
            >
              <Icon type="delete" />
            </Popconfirm>
          </span >),
      },
    ];

  render() {
    const { categories, totals } = this.props;
    return (
      <Table
        columns={this.columns}
        dataSource={categories.map((post, key) => { return { ...post, key }; })}
        pagination={{ pageSize: TABLE_PAGESIZE, total: totals, onChange: this.onPageNumberChange }}
      />
    );
  }
}
