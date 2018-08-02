import React from 'react';
import { Table, Icon, Divider, Card, Button, Spin, Popconfirm } from 'antd';
import { Link } from 'dva/router';

export default class index extends React.Component {
  columns =
    [
      {
        title: 'ID',
        dataIndex: 'postId',
        key: 'postId',
        render: val => <Link to={`/category-management/detail/${val}`}>{val}</Link>,
      },
      {
        title: 'Tên danh mục',
        dataIndex: 'title',
        key: 'title',
      },
      {
        title: 'Ngày đăng',
        dataIndex: 'dayPost',
        key: 'dayPost',
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
    const { posts } = this.props;
    return (
      <Table
        columns={this.columns}
        dataSource={posts.map((post, key) => { return { ...post, key }; })}
        pagination={{ pageSize: 10 }}
      />
    );
  }
}
