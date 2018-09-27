import React from 'react';
import { connect } from 'dva';
import { routerRedux } from 'dva/router';
import { Button, Popconfirm, Row, Col } from 'antd';
import moment from 'moment';
import styles from './index.less';
import { DateFormatDisplay, ENG_VN_DICTIONARY, STATUSES } from '../../../common/constants';

@connect(({ modals, postManagement }) => ({
  ...modals, postManagement,
}))

export default class index extends React.Component {
  componentDidMount() {
    const { dispatch } = this.props;
    const { id } = this.props.match.params;
    dispatch({
      type: 'postManagement/fetchPostInformation',
      payload: { id },
    });
  }

  handleRedirectToCategoryDetail = (category) => {
    const { dispatch } = this.props;
    dispatch({
      type: 'categoryManagement/getACategory',
      payload: {
        id: category.id,
      },
    });
    dispatch(routerRedux.push(`/category-management/detail/${category.id}`));
  }

  handleDateAndTimeFormat = (date) => {
    return (moment.utc(date).local().format(DateFormatDisplay));
  }

  handleRedirectToUserDetail = (user) => {
    const { dispatch } = this.props;
    dispatch({
      type: 'userManagement/getProfile',
      payload: {
        id: user.id,
      },
    });
    dispatch(routerRedux.push(`/user-management/detail/${user.id}`));
  }

  handleChangeCMSStatus = (e) => {
    const { dispatch } = this.props;
    const { id } = this.props.match.params;
    const { statusCMS } = this.props.postManagement.postInformation;
    const newStatusCMS = statusCMS === STATUSES.Blocked ? STATUSES.Activated : STATUSES.Blocked;
    dispatch({
      type: 'postManagement/changeAPostCMSStatus',
      payload: { newStatusCMS, id },
    });
  }

  handleActionWithPost = (record) => {
    let buttonContent = 'Khóa';
    let buttonIcon = 'lock';
    let newPostStatus = STATUSES.Blocked;
    let popConfirmTitle = 'Bạn chắc chắn muốn khóa bài đăng này?';
    if (record.statusCMS === STATUSES.Blocked) {
      buttonContent = 'Mở khóa';
      buttonIcon = 'unlock';
      newPostStatus = STATUSES.Activated;
      popConfirmTitle = 'Bạn có muốn mở lại bài đăng này?';
    }
    return (
      <span>
        <Popconfirm
          title={popConfirmTitle}
          onConfirm={() => this.handleChangeCMSStatus(record.key)}
        >
          <Button type="primary" icon={buttonIcon} className={styles.buttonStyle}>{buttonContent}</Button>
        </Popconfirm>
      </span >
    );
  }

  displayImageColumn = (images) => {
    return (
      <div>
        {images && images.map((image, index) => {
          return (<img key={index} src={image && image.originalImage} alt="" className={styles.imageBox}/>);
        })}
      </div>
    );
  }

  displayImage = (postInformation) => {
    if ((postInformation.images && postInformation.images.length === 0) || (postInformation.images && postInformation.images === null)) {
      return (<img src="./images/noImage.jpg" alt="" />);
    } else {
      const images = postInformation.images && postInformation.images;
      let count = images && Math.floor((images.length) / 4);

      if (count === 0) {
        count = (images.length) % 4;
        return (
          <div className={styles.row}>
            {images && images.map((image) => {
              return (
                  <div className={styles.column}>
                    <img src={image.originalImage} alt="" className={styles.imageBox} />
                  </div>
              );
            })}
          </div>
        );
      } else {
        const columns = [count, count, count, count];
        const extra = images && (images.length) % 4;
        for (let i = 0; i < extra; i++) {
          columns[i]++;
        }
        console.log(count);
        return (
          <div className={styles.row}>
            <div className={styles.column}>
              {this.displayImageColumn(images && images.slice(0, columns[0]))}
            </div>
            <div className={styles.column}>
              {this.displayImageColumn(images && images.slice(columns[0], columns[0] + columns[1]))}
            </div>
            <div className={styles.column}>
              {this.displayImageColumn(images && images.slice(columns[0] + columns[1], columns[0] + columns[1] + columns[2]))}
            </div>
            <div className={styles.column}>
              {this.displayImageColumn(images && images.slice(columns[0] + columns[1] + columns[2], columns[0] + columns[1] + columns[2] + columns[3]))}
            </div>
          </div>
        );
      }
    }
  }

  renderDetail(postInformation) {
    const { id, user, title, description, images, address, createdTime, statusCMS, statusApp, category } = postInformation !== null ? postInformation : null;
    return (
      <div>
        <div className="containerHeader">
          <h1>Chi tiết bài đăng</h1>
          <div className="rightButton">
            {this.handleActionWithPost(postInformation)}
          </div>
        </div>
        <div className="containerBody">
          <Row>
            <Col span={8}><span> Tiêu đề </span></Col>
            <Col span={8}><span> Người đăng </span></Col>
          </Row>
          <Row>
            <Col span={8}><p> {title} </p></Col>
            <Col span={8}><p> <a className={styles.detailText} onClick={() => this.handleRedirectToUserDetail(user)}> {user.firstName} {user.lastName}</a></p></Col>
          </Row>

          <Row>
            <Col span={8}><span> Địa chỉ </span></Col>
            <Col span={8}><span> Danh mục </span></Col>
          </Row>
          <Row>
            <Col span={8}><p> {address.provinceCityName} </p></Col>
            <Col span={8}><p> <a className={styles.detailText} onClick={() => this.handleRedirectToCategoryDetail(category)}> {category.categoryName}</a></p></Col>
          </Row>

          <Row>
            <Col span={8}><span> Trạng thái </span></Col>
            <Col span={8}><span> Trạng thái trên ứng dụng di động</span></Col>
          </Row>
          <Row>
            <Col span={8}><p className={styles.statusText}> {ENG_VN_DICTIONARY[statusCMS]}</p></Col>
            <Col span={8}><p className={styles.statusText}> {ENG_VN_DICTIONARY[statusApp]}</p></Col>
          </Row>

          <Row>
            <Col span={8}><span> Ngày tạo </span></Col>
          </Row>
          <Row>
            <Col span={8}><p> {this.handleDateAndTimeFormat(createdTime)} </p></Col>
          </Row>

          <Row>
            <Col span={8}><span> Mô tả </span></Col>
          </Row>
          <Row>
            <Col><p> {description} </p></Col>
          </Row>
          <br /><br /><br /><br />
          <Row>
            <div>
              {this.displayImage(postInformation)}
            </div>
          </Row>
        </div>
      </div>
    );
  }

  render() {
    const { postManagement: { postInformation } } = this.props;
    const postDetail = postInformation && this.renderDetail(postInformation);
    return (
      <div>
        {postDetail}
      </div>
    );
  }
}
