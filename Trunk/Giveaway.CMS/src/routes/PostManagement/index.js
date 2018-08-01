import React from 'react';
import { connect } from 'dva';
import { Table, Icon, Divider, Card, Button, Spin, Popconfirm } from 'antd';
import { Link, routerRedux } from 'dva/router';
import EditableTable from "../../components/Common/Table/EditableTable";

const columns =
  [
    {
      title: 'ID',
      dataIndex: 'postId',
      key: 'postId',
      render: val => <Link to={`/post-management/detail/${val}`}>{val}</Link>,
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
    },
    {
      title: 'Category',
      dataIndex: 'category',
      key: 'category',
    }, {
      title: 'Day Post',
      dataIndex: 'dayPost',
      key: 'dayPost',
    },
    {
      title: 'Action',
      dataIndex: 'dayPost',
      render: () => (
        <span>
          <Popconfirm title="Sure to delete?" onConfirm={(key) => {
            const dataSource = [...this.state.dataSource];
            this.setState({ dataSource: dataSource.filter(item => item.key !== key) });
          }}>
            <Button>
              <Icon type="delete" />
            </Button>
          </Popconfirm>
        </span >),
      handleDelete: (key) => {
        console.log(key);
        const dataSource = [...this.state.dataSource];
        this.setState({ dataSource: dataSource.filter(item => item.key !== key) });
      },
      // handleDeletePost: dispatch => {
      //   dispatch({
      //     type: 'postManagement/deleteAPost',
      //     payload: {}
      //   })
      // }
    },
  ];

@connect(({ modals, postManagement }) => ({
  ...modals, ...postManagement,
}))
export default class index extends React.Component {
  state = {};
  componentWillMount() {
    // this.props.dispatch({
    //   type: 'postManagement/fetchPost',
    //   payload: {},
    // });
  }



  render() {
    // const { posts } = this.props;
    return (
      <div>
        <div className="containerHeader">
          <h1>Post Management</h1>
        </div>
        <div className="containerBody">
          {/* <Table
            columns={columns}
            dataSource={posts}
            pagination={{ pageSize: 10 }}
          /> */}
          <EditableTable />
        </div>
      </div>
    );
  }
}
