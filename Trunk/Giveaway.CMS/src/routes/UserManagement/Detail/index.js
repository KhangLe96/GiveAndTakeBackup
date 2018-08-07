import React from 'react';
import { connect } from 'dva';
import { routerRedux } from 'dva/router';
import { Button, Popconfirm } from 'antd';

@connect(({ modals, userManagement }) => ({
  ...modals, ...userManagement,
}))

export default class index extends React.Component {
  constructor(props) {
    super(props);
    this.renderDetail = this.renderDetail.bind(this);
  }
  state = {
    activateStatus: 'Activated',
    result: null,
  }

  componentDidMount() {
    const { users, dispatch } = this.props;
    // TODO: get data theo id
    if (users.length === 0) {
      dispatch({
        type: 'userManagement/fetch',
        payload: {},
      });
    }
  }

  componentWillReceiveProps(nextProps) {
    const userNo = nextProps.match.params.id;
    // TODO : Delete
    const result = nextProps.users ? nextProps.users.filter((val) => {
      return val.id === userNo;
    }) : [];

    if (result !== null) {
      this.setState({
        result,
        activateStatus: result[0].status,
      });
    }
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

  handleOnConfirm = () => {}

  renderDetail(newResult) {
    const { status, id, username, firstName, lastName, birthdate, email, phoneNumber, gender, address, role } = newResult !== null ? newResult : null;
    return (
      <div>
        <div className="containerHeader">
          <h1>Thông Tin Chi tiết</h1>
          <div className="rightButton">
            <Popconfirm
              title="Bạn chắc chắn muốn block user này ?"
              onConfirm={() => {
                  const { dispatch, users } = this.props;
                  // this.setState({
                  //   activateStatus:
                  // });
                  const newStatus = status === 'Blocked' ? 'Activated' : 'Blocked';
                  console.log(newStatus);
                  dispatch({
                      type: 'userManagement/changeStatus',
                      payload: { newStatus, id },
                  });
                }}
            >
              <Button type="primary">
                { this.state.activateStatus === 'Activated' ? 'Block' : 'Activate' }
              </Button>
            </Popconfirm>
          </div>

        </div>
        <div className="containerBody">
          <h3> user Id              : {id}</h3>
          <h3> UserName             : {username} </h3>
          <h3> Tên                  : {firstName} </h3>
          <h3> Họ                   : {lastName} </h3>
          <h3> Ngày sinh            : {birthdate} </h3>
          <h3> Email                : {email} </h3>
          <h3> Số điện thoại        : {phoneNumber} </h3>
          <h3> Giới tính            : {gender} </h3>
          <h3> Địa chỉ              : {address} </h3>
          <h3> Role                 : {role} </h3>
          <h3> Trạng thái           : {status} </h3>
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
    );
  }

  render() {
    const { result } = this.state;
    const test = (result !== null) ? this.renderDetail(result[0]) : null;
    return (
      <div>
        { test }
      </div>
    );
  }
}
