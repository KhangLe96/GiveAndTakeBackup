import React from 'react';
import { connect } from 'dva';
import { routerRedux } from 'dva/router';
import { Button, Popconfirm } from 'antd';

@connect(({ modals, userManagement }) => ({
  ...modals, ...userManagement,
}))
export default class index extends React.Component {
  componentDidMount() {
    const { users, dispatch } = this.props;
    if (users.length === 0) {
      dispatch({
        type: 'userManagement/fetch',
        payload: {},
      });
    }
    console.log(this.props.users);
  }

  navigateToEditPage = () => {
    const { dispatch, users, match: { params }, history } = this.props;
    dispatch({
      type: 'userManagement/deleteuser',
      payload: {
        id: params.id,
        users,
      },
    });
    dispatch(routerRedux.push('/user-management'));
  }

  render() {
    const { users } = this.props;
    const userNo = this.props.match.params.id;
    const result = users ? users.filter((val) => {
      return val.id === userNo;
    }) : [];
    return (result[0])
      ? (
        <div>
          <div className="containerHeader">
            <h1>Thông Tin Chi tiết</h1>
            <div className="rightButton">
              <Popconfirm
                title= "Bạn chắc chắn muốn block user này ?"
                onConfirm={() => {
                const { dispatch, users } = this.props;
                const id = this.props.match.params;
                const status = 'Blocked';
                dispatch({
                  type: 'userManagement/changeStatus',
                  payload: { status, ...id },
                });
              }}
              >
                <Button type="primary">
                  Block
                </Button>
              </Popconfirm>
            </div>

          </div>
          <div className="containerBody">
            <h3> user Id              : {result[0].id}</h3>
            <h3> UserName             : {result[0].username} </h3>
            <h3> Tên                  : {result[0].firstName} </h3>
            <h3> Họ                   : {result[0].lastName} </h3>
            <h3> Ngày sinh            : {result[0].birthdate} </h3>
            <h3> Email                : {result[0].email} </h3>
            <h3> Số điện thoại        : {result[0].phoneNumber} </h3>
            <h3> Giới tính            : {result[0].gender} </h3>
            <h3> Địa chỉ              : {result[0].address} </h3>
            <h3> Role                 : {result[0].role} </h3>
            <h3> Trạng thái           : {result[0].status} </h3>
            <div>
              {/* {(result[0].avatarUrl)
                ?
                result[0].avatarUrl.map((avatarPath) => {
                  return <img src={avatarPath} key={avatarPath} alt="" height="400" width="400" />;
                })

                : null} */}
            </div>
          </div>
        </div>
      ) : null;
  }
}
