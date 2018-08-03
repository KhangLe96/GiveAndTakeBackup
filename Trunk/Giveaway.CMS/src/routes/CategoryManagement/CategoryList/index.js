import React from 'react';
import { Table, Icon, Divider, Card, Button, Spin, Popconfirm } from 'antd';
import { Link, routerRedux } from 'dva/router';

export default class index extends React.Component {
  constructor(props) {
    super(props);
    this.onPageNumberChange = this.onPageNumberChange.bind(this);
  }

  onPageNumberChange(page, pageSize) {
    const { dispatch } = this.props;
    const payload = { page, limit: pageSize };
    dispatch({
      type: 'management/fetchCategory',
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
    const { categories } = this.props;
    return (
      <Table
        columns={this.columns}
        dataSource={categories.map((post, key) => { return { ...post, key }; })}
        pagination={{ pageSize: 10, total: 11, onChange: this.onPageNumberChange }}
      />
    );
  }
}
