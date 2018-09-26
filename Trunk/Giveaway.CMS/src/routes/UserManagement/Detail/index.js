import React from 'react';
import { connect } from 'dva';
import { Button, Popconfirm, Row, Col } from 'antd';
import { Record } from '../../../../node_modules/immutable';
import moment from 'moment';
import styles from './index.less';
import { DateFormatDisplay, STATUSES, STATUS_ACTION_ACTIVATE, STATUS_ACTION_BLOCK, ENG_VN_DICTIONARY } from '../../../common/constants';

@connect(({ modals, userManagement }) => ({
  ...modals, userManagement,
}))

export default class index extends React.Component {
  componentDidMount() {
    const { dispatch } = this.props;
    const { id } = this.props.match.params;
    dispatch({
      type: 'userManagement/getProfile',
      payload: { id },
    });
  }

  handleOnConfirm = (e) => {
    const { dispatch } = this.props;
    const { id } = this.props.match.params;
    const { status } = this.props.userManagement.userProfile;
    const newStatus = status === STATUSES.Blocked ? STATUSES.Activated : STATUSES.Blocked;
    dispatch({
      type: 'userManagement/changeStatusProfile',
      payload: { newStatus, id },
    });
  }

  handleDisplayStatusButton = (userProfile) => {
    return (userProfile.status === STATUSES.Activated ? STATUS_ACTION_BLOCK : STATUS_ACTION_ACTIVATE);
  }

  handleDateAndTimeFormat = (date) => {
    return (moment.utc(date).local().format(DateFormatDisplay));
  }

  handleActionWithUser = (record) => {
    let buttonContent = 'Khóa';
    let buttonIcon = 'lock';
    let newStatus = STATUSES.Blocked;
    let popConfirmTitle = 'Bạn chắc chắn muốn khóa User này?';
    if (record.status === STATUSES.Blocked) {
      buttonContent = STATUS_ACTION_ACTIVATE;
      buttonIcon = 'unlock';
      newStatus = STATUSES.Activated;
      popConfirmTitle = 'Bạn có muốn mở lại User này?';
    }
    return (
      <span>
        <Popconfirm
          title={popConfirmTitle}
          onConfirm={() => this.handleOnConfirm(record.key)}
        >
          <Button type="primary" icon={buttonIcon} className={styles.buttonStyle}>{buttonContent}</Button>
        </Popconfirm>
      </span >
    );
  }

  displayImage = (avatarUrl) => {
    if ((avatarUrl === 'string') || (avatarUrl === null)) {
      return (<img src="./images/noImage.jpg" alt="" className={styles.avatarStyle} />)
    }
    else {
      return (<img src={avatarUrl} alt="" className={styles.avatarStyle} />)
    }
  }

  renderDetail(userProfile) {
    const { username, status, firstName, lastName, birthdate, email, phoneNumber, gender, address, role, avatarUrl } = userProfile !== null ? userProfile : null;
    return (
      <div>
        <div className="containerHeader">
          <h1>Xem thông tin thành viên</h1>
          <div className="rightButton">
            {this.handleActionWithUser(userProfile)}
          </div>
        </div>
        <div className="containerBody">
          <Row>
            <Col span={8} className={styles.imageBox}>
              {this.displayImage(avatarUrl)}
              <br /><br />
              <Row>
                <Col align="middle"><h2> {firstName} {lastName} </h2></Col>
              </Row>
            </Col>
            <Col span={16} className={styles.inforBox}>
              <Row>
                <Col span={12}><span> Tên người dùng </span></Col>
                <Col span={12}><span> Điện thoại </span></Col>
              </Row>
              <Row>
                <Col span={12}><p>{firstName} {lastName}</p></Col>
                <Col span={12}><p>{phoneNumber}</p></Col>
              </Row>
              <br /><br />
              <Row>
                <Col span={12}><span> Email </span></Col>
                <Col span={12}><span> Giới tính </span></Col>
              </Row>
              <Row>
                <Col span={12}><p>{email}</p></Col>
                <Col span={12}><p>{ENG_VN_DICTIONARY[gender]}</p></Col>
              </Row>
              <br /><br />
              <Row>
                <Col span={12}><span> Ngày sinh </span></Col>
                <Col span={12}><span> Địa chỉ </span></Col>
              </Row>
              <Row>
                <Col span={12}><p>{this.handleDateAndTimeFormat(birthdate)}</p></Col>
                <Col span={12}><p>{address}</p></Col>
              </Row>
              <br /><br />
              <Row>
                <Col span={12}><span> Vai trò </span></Col>
                <Col span={12}><span> Trạng thái </span></Col>
              </Row>
              <Row>
                <Col span={12}><p>{ENG_VN_DICTIONARY[role && role[0]]} {ENG_VN_DICTIONARY[role && role[1]]}</p></Col>
                <Col span={12}><p className={styles.statusText}>{ENG_VN_DICTIONARY[status]}</p></Col>
              </Row>
            </Col>
          </Row>
        </div>
      </div>

    );
  }
  render() {
    const { userManagement: { userProfile } } = this.props;
    const userDetail = (userProfile !== null) ? this.renderDetail(userProfile) : null;
    return (
      <div>
        {userDetail}
      </div>
    );
  }
}
