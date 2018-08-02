import React from 'react';
import { Table, Icon, Divider, Card, Button, Spin, Popconfirm, Input } from 'antd';
import { Link } from 'dva/router';


function onChange(pagination, filters, sorter) {
  console.log('params', pagination, filters, sorter);
}

export default class index extends React.Component {

  columns =
    [
      {
        title: 'ID',
        dataIndex: 'postId',
        key: 'postId',
        render: val => <Link to={`/post-management/detail/${val}`}>{val}</Link>,
        defaultSortOrder: 'descend',
        sorter: (a, b) => a.postId - b.postId,
      },
      {
        title: 'Title',
        dataIndex: 'title',
        key: 'title',
      },
      {
        title: 'Address',
        dataIndex: 'postAddress',
        key: 'postAddress',
        filters: [{
          text: 'Da Nang',
          value: 'Da Nang',
        }, {
          text: 'Ha Noi',
          value: 'Ha Noi',
        }],
        filterMultiple: true,
        onFilter: (value, record) => record.postAddress.indexOf(value) === 0,
      },
      {
        title: 'Category',
        dataIndex: 'category',
        key: 'category',
        filters: [{
          text: 'car',
          value: 'car',
        }, {
          text: 'pen',
          value: 'pen',
        }, {
          text: 'furniture',
          value: 'furniture'
        }],
        filterMultiple: true,
        onFilter: (value, record) => record.category.indexOf(value) === 0,
      }, {
        title: 'Day Post',
        dataIndex: 'dayPost',
        key: 'dayPost',
      }, {
        title: 'User',
        dataIndex: 'userPost',
        key: 'userPost', 
      }, {
        title: 'Action',
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
            <button>
              <Icon type="delete" />
            </button>
            </Popconfirm>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <Popconfirm
              title="Ban User?"
            >
            <button>
              <Icon type="close" />
            </button>
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
        onChange={onChange}
      />
    );
  }
}
